using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class WorkerScheduleService : IWorkerScheduleService
    {
        private readonly KhoThoContext _context;
        private readonly IMapper _mapper;

        public WorkerScheduleService(KhoThoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkerScheduleDto>> GetAllSchedulesAsync()
        {
            var schedules = await _context.WorkerSchedules.ToListAsync();
            return _mapper.Map<IEnumerable<WorkerScheduleDto>>(schedules);
        }

        public async Task<WorkerScheduleDto> GetScheduleByIdAsync(int id)
        {
            var schedule = await _context.WorkerSchedules.FindAsync(id);
            return _mapper.Map<WorkerScheduleDto>(schedule);
        }

        public async Task<IEnumerable<WorkerScheduleDto>> GetSchedulesByWorkerIdAsync(int workerId)
        {
            var schedules = await _context.WorkerSchedules
                .Where(s => s.WorkerId == workerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<WorkerScheduleDto>>(schedules);
        }

        public async Task<WorkerScheduleDto> CreateScheduleAsync(CreateWorkerScheduleDto dto)
        {
            var schedule = _mapper.Map<WorkerSchedule>(dto);
            _context.WorkerSchedules.Add(schedule);
            await _context.SaveChangesAsync();
            return _mapper.Map<WorkerScheduleDto>(schedule);
        }

        public async Task<WorkerScheduleDto> UpdateScheduleAsync(int id, CreateWorkerScheduleDto dto)
        {
            var schedule = await _context.WorkerSchedules.FindAsync(id);
            if (schedule == null) return null;

            _mapper.Map(dto, schedule);
            await _context.SaveChangesAsync();
            return _mapper.Map<WorkerScheduleDto>(schedule);
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await _context.WorkerSchedules.FindAsync(id);
            if (schedule == null) return false;

            _context.WorkerSchedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
