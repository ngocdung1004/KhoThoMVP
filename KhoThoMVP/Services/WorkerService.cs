using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly KhoThoContext _context;
        private readonly IMapper _mapper;

        public WorkerService(KhoThoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkerDto>> GetAllWorkersAsync()
        {
            var workers = await _context.Workers
                .Include(w => w.User)
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
        public async Task<WorkerDto> CreateWorkerAsync(CreateWorkerDto createWorkerDto)
        {
            var workerDto = new WorkerDto
            {
                UserId = createWorkerDto.UserId,
                ExperienceYears = createWorkerDto.ExperienceYears,
                Rating = createWorkerDto.Rating,
                Bio = createWorkerDto.Bio,
                Verified = createWorkerDto.Verified
            };

            var worker = _mapper.Map<Worker>(workerDto);
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            return _mapper.Map<WorkerDto>(worker);
        }

        //public async Task<WorkerDto> UpdateWorkerAsync(int id, WorkerDto workerDto)
        //{
        //    var worker = await _context.Workers.FindAsync(id);
        //    if (worker == null)
        //        throw new KeyNotFoundException($"Worker with ID {id} not found");

        //    _mapper.Map(workerDto, worker);
        //    await _context.SaveChangesAsync();

        //    return _mapper.Map<WorkerDto>(worker);
        //}

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
                .Include(w => w.WorkerJobTypes)
                .Include(w => w.Subscriptions)
                .Include(w => w.Reviews)
                .Include(w => w.Payments)
                .FirstOrDefaultAsync(w => w.WorkerId == id);

            if (worker == null)
                throw new KeyNotFoundException($"Worker with ID {id} not found");

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
    }
}
