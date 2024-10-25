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

        public async Task<WorkerDto> UpdateWorkerAsync(int id, WorkerDto workerDto)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
                throw new KeyNotFoundException($"Worker with ID {id} not found");

            _mapper.Map(workerDto, worker);
            await _context.SaveChangesAsync();

            return _mapper.Map<WorkerDto>(worker);
        }

        public async Task DeleteWorkerAsync(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
                throw new KeyNotFoundException($"Worker with ID {id} not found");

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
