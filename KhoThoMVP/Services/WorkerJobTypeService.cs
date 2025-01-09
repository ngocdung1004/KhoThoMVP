using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class WorkerJobTypeService : IWorkerJobTypeService
    {
        private readonly DungnnExe201Thodung5Context _context;
        private readonly IMapper _mapper;

        public WorkerJobTypeService(DungnnExe201Thodung5Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkerJobTypeDto>> GetAllWorkerJobTypesAsync()
        {
            var workerJobTypes = await _context.WorkerJobTypes.ToListAsync();
            return _mapper.Map<IEnumerable<WorkerJobTypeDto>>(workerJobTypes);
        }

        public async Task<WorkerJobTypeDto> GetWorkerJobTypeByIdAsync(int id)
        {
            var workerJobType = await _context.WorkerJobTypes.FindAsync(id);
            return _mapper.Map<WorkerJobTypeDto>(workerJobType);
        }

        public async Task<IEnumerable<WorkerJobTypeDto>> GetWorkerJobTypesByWorkerIdAsync(int workerId)
        {
            var workerJobTypes = await _context.WorkerJobTypes
                .Where(wjt => wjt.WorkerId == workerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<WorkerJobTypeDto>>(workerJobTypes);
        }

        public async Task<WorkerJobTypeDto> CreateWorkerJobTypeAsync(WorkerJobTypeDto workerJobTypeDto)
        {
            var workerJobType = _mapper.Map<WorkerJobType>(workerJobTypeDto);
            _context.WorkerJobTypes.Add(workerJobType);
            await _context.SaveChangesAsync();
            return _mapper.Map<WorkerJobTypeDto>(workerJobType);
        }

        public async Task<WorkerJobTypeDto> UpdateWorkerJobTypeAsync(int id, WorkerJobTypeDto workerJobTypeDto)
        {
            var workerJobType = await _context.WorkerJobTypes.FindAsync(id);
            if (workerJobType == null) return null;

            _mapper.Map(workerJobTypeDto, workerJobType);
            await _context.SaveChangesAsync();
            return _mapper.Map<WorkerJobTypeDto>(workerJobType);
        }

        public async Task DeleteWorkerJobTypeAsync(int id)
        {
            var workerJobType = await _context.WorkerJobTypes.FindAsync(id);
            if (workerJobType != null)
            {
                _context.WorkerJobTypes.Remove(workerJobType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
