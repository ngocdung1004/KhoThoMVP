using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class UserService : IUserService
    {
        private readonly DungnnExe201Thodung5Context _context;
        private readonly IMapper _mapper;

        public UserService(DungnnExe201Thodung5Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            _mapper.Map(userDto, user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        //public async Task DeleteUserAsync(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //        throw new KeyNotFoundException($"User with ID {id} not found");

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //}
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Worker)
                    .ThenInclude(w => w.WorkerJobTypes)
                .Include(u => u.Worker)
                    .ThenInclude(w => w.Subscriptions)
                .Include(u => u.Worker)
                    .ThenInclude(w => w.Payments)
                .Include(u => u.Worker)
                    .ThenInclude(w => w.Reviews)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            // If user is a worker, remove all worker-related data first
            if (user.Worker != null)
            {
                if (user.Worker.WorkerJobTypes != null)
                    _context.WorkerJobTypes.RemoveRange(user.Worker.WorkerJobTypes);

                if (user.Worker.Subscriptions != null)
                    _context.Subscriptions.RemoveRange(user.Worker.Subscriptions);

                if (user.Worker.Payments != null)
                    _context.Payments.RemoveRange(user.Worker.Payments);

                if (user.Worker.Reviews != null)
                    _context.Reviews.RemoveRange(user.Worker.Reviews);

                _context.Workers.Remove(user.Worker);
            }

            // Remove user's reviews (as a customer)
            if (user.Reviews != null)
                _context.Reviews.RemoveRange(user.Reviews);

            // Finally remove the user
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new KeyNotFoundException($"User with email {email} not found");
            return _mapper.Map<UserDto>(user);
        }
    }
}
