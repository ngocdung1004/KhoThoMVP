using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class UserService : IUserService
    {
        private readonly KhoThoContext _context;
        private readonly IMapper _mapper;

        public UserService(KhoThoContext context, IMapper mapper)
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

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

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
