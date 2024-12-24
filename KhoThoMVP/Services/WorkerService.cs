using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KhoThoMVP.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly KhoThoContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public WorkerService(KhoThoContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<IEnumerable<WorkerDto>> GetAllWorkersAsync()
        {
            var workers = await _context.Workers
                .Include(w => w.User)
                .Where(w => w.User.UserType == 2)
                .ToListAsync();
            return _mapper.Map<IEnumerable<WorkerDto>>(workers);
        }

        public async Task<WorkerDto> GetWorkerByIdAsync(int id)
        {
            var worker = await _context.Workers
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.WorkerId == id);
            if (worker == null)
                throw new KeyNotFoundException($"Worker with ID {id} not found");
            return _mapper.Map<WorkerDto>(worker);
        }

        public async Task<WorkerDto> CreateWorkerAsync(WorkerDto workerDto)
        {
            var worker = _mapper.Map<Worker>(workerDto);
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return _mapper.Map<WorkerDto>(worker);
        }
        //public async Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto createWorkerDto)
        //{
        //    // Tìm người dùng tương ứng
        //    var user = await _context.Users.FindAsync(createWorkerDto.UserId);
        //    if (user == null)
        //    {
        //        throw new KeyNotFoundException($"User with ID {createWorkerDto.UserId} not found");
        //    }

        //    // Cập nhật UserType của người dùng thành 2
        //    user.UserType = 2;
        //    var workerDto = new WorkerDto
        //    {
        //        UserId = createWorkerDto.UserId,
        //        ExperienceYears = createWorkerDto.ExperienceYears,
        //        Rating = createWorkerDto.Rating,
        //        Bio = createWorkerDto.Bio,
        //        Verified = createWorkerDto.Verified
        //    };

        //    var worker = _mapper.Map<Worker>(workerDto);
        //    _context.Workers.Add(worker);
        //    await _context.SaveChangesAsync();

        //    return _mapper.Map<WorkerDto>(worker);
        //}

        //public async Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto createWorkerDto)
        //{
        //    // Tìm người dùng tương ứng
        //    var user = await _context.Users.FindAsync(createWorkerDto.UserId);
        //    if (user == null) // Sửa điều kiện này
        //    {
        //        throw new KeyNotFoundException($"User with ID {createWorkerDto.UserId} not found");
        //    }

        //    // Kiểm tra xem worker đã tồn tại chưa
        //    var existingWorker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == createWorkerDto.UserId);
        //    if (existingWorker != null)
        //    {
        //        throw new InvalidOperationException($"Worker with User ID {createWorkerDto.UserId} already exists.");
        //    }

        //    // Cập nhật UserType của người dùng thành 2
        //    user.UserType = 2;

        //    // Tạo worker từ DTO
        //    var worker = _mapper.Map<Worker>(createWorkerDto);
        //    _context.Workers.Add(worker);
        //    await _context.SaveChangesAsync(); // Lưu worker trước để có WorkerId

        //    // Liên kết các loại công việc
        //    foreach (var jobTypeId in createWorkerDto.JobTypeIds)
        //    {
        //        var workerJobType = new WorkerJobType
        //        {
        //            WorkerId = worker.WorkerId,
        //            JobTypeId = jobTypeId
        //        };
        //        _context.WorkerJobTypes.Add(workerJobType);
        //    }
        //    await _context.SaveChangesAsync(); // Lưu các liên kết loại công việc

        //    return _mapper.Map<WorkerDto>(worker);
        //}



        //public async Task<WorkerDto> UpdateWorkerAsync(int id, WorkerDto workerDto)
        //{
        //    var worker = await _context.Workers.FindAsync(id);
        //    if (worker == null)
        //        throw new KeyNotFoundException($"Worker with ID {id} not found");

        //    _mapper.Map(workerDto, worker);
        //    await _context.SaveChangesAsync();

        //    return _mapper.Map<WorkerDto>(worker);
        //}

        private async Task<string> SaveImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                return null;

            // Tạo tên file độc nhất
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Tạo đường dẫn đến thư mục lưu trữ
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", folder);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Đường dẫn đầy đủ đến file
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Lưu file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn tương đối
            return $"/uploads/{folder}/{fileName}";
        }

        public async Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto createWorkerDto)
        {
            // Kiểm tra user
            var user = await _context.Users.FindAsync(createWorkerDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {createWorkerDto.UserId} not found");
            }

            // Kiểm tra worker đã tồn tại
            var existingWorker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == createWorkerDto.UserId);
            if (existingWorker != null)
            {
                throw new InvalidOperationException($"Worker with User ID {createWorkerDto.UserId} already exists.");
            }

            // Cập nhật UserType
            user.UserType = 2;

            // Tạo worker mới
            var worker = new Worker
            {
                UserId = createWorkerDto.UserId,
                ExperienceYears = createWorkerDto.ExperienceYears,
                Rating = createWorkerDto.Rating,
                Bio = createWorkerDto.Bio,
                Verified = createWorkerDto.Verified
            };

            // Xử lý upload ảnh
            if (createWorkerDto.ProfileImage != null)
            {
                worker.ProfileImage = await SaveImageAsync(createWorkerDto.ProfileImage, "profile-images");
            }

            if (createWorkerDto.FrontIdcard != null)
            {
                worker.FrontIdcard = await SaveImageAsync(createWorkerDto.FrontIdcard, "id-cards");
            }

            if (createWorkerDto.BackIdcard != null)
            {
                worker.BackIdcard = await SaveImageAsync(createWorkerDto.BackIdcard, "id-cards");
            }

            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            // Thêm các loại công việc
            foreach (var jobTypeId in createWorkerDto.JobTypeIds)
            {
                var workerJobType = new WorkerJobType
                {
                    WorkerId = worker.WorkerId,
                    JobTypeId = jobTypeId
                };
                _context.WorkerJobTypes.Add(workerJobType);
            }
            await _context.SaveChangesAsync();

            return _mapper.Map<WorkerDto>(worker);
        }
    


    public async Task<WorkerDto> UpdateWorkerAsync(int id, WorkerDto workerDto)
        {
            var worker = await _context.Workers
                .Include(w => w.User)  // Include User để tránh mất data khi map
                .FirstOrDefaultAsync(w => w.WorkerId == id);

            if (worker == null)
                throw new KeyNotFoundException($"Worker with ID {id} not found");

            // Chỉ cập nhật các thuộc tính của Worker
            worker.ExperienceYears = workerDto.ExperienceYears;
            worker.Rating = workerDto.Rating;
            worker.Bio = workerDto.Bio;
            worker.Verified = workerDto.Verified;

            await _context.SaveChangesAsync();

            return _mapper.Map<WorkerDto>(worker);
        }

        //public async Task DeleteWorkerAsync(int id)
        //{
        //    var worker = await _context.Workers.FindAsync(id);
        //    if (worker == null)
        //        throw new KeyNotFoundException($"Worker with ID {id} not found");

        //    _context.Workers.Remove(worker);
        //    await _context.SaveChangesAsync();
        //}
        public async Task DeleteWorkerAsync(int id)
        {
            var worker = await _context.Workers
                .Include(w => w.User) // Bao gồm thông tin người dùng
                .Include(w => w.WorkerJobTypes)
                .Include(w => w.Subscriptions)
                .Include(w => w.Reviews)
                .Include(w => w.Payments)
                .FirstOrDefaultAsync(w => w.WorkerId == id);

            if (worker == null)
                throw new KeyNotFoundException($"Worker with ID {id} not found");

            // Cập nhật UserType của người dùng thành 1
            if (worker.User != null)
            {
                worker.User.UserType = 1; // Đặt UserType thành 1
            }

            // Remove related records first
            if (worker.WorkerJobTypes != null)
                _context.WorkerJobTypes.RemoveRange(worker.WorkerJobTypes);

            if (worker.Subscriptions != null)
                _context.Subscriptions.RemoveRange(worker.Subscriptions);

            if (worker.Reviews != null)
                _context.Reviews.RemoveRange(worker.Reviews);

            if (worker.Payments != null)
                _context.Payments.RemoveRange(worker.Payments);

            // Finally remove the worker
            _context.Workers.Remove(worker);

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<WorkerDto>> GetWorkersByJobTypeAsync(int jobTypeId)
        {
            var workers = await _context.WorkerJobTypes
                .Include(wjt => wjt.Worker)
                .ThenInclude(w => w.User)
                .Where(wjt => wjt.JobTypeId == jobTypeId)
                .Select(wjt => wjt.Worker)
                .ToListAsync();

            return _mapper.Map<IEnumerable<WorkerDto>>(workers);
        }


        public async Task<WorkerDto> GetWorkerByEmailAsync(string email)
        {
            var worker = await _context.Workers
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.User.Email == email);

            if (worker == null)
                throw new KeyNotFoundException($"Worker with email {email} not found");

            return new WorkerDto
            {
                WorkerId = worker.WorkerId,
                UserId = worker.UserId,
                ExperienceYears = worker.ExperienceYears,
                Rating = (float?)worker.Rating,
                Bio = worker.Bio,
                Verified = worker.Verified,
                User = new UserDto
                {
                    UserId = worker.User.UserId,
                    Email = worker.User.Email,
                    FullName = worker.User.FullName,
                    PhoneNumber = worker.User.PhoneNumber,
                    ProfilePicture = worker.User.ProfilePicture,
                    UserType = worker.User.UserType,
                    // Add other user properties as needed
                }
            };
        }
    }
}
