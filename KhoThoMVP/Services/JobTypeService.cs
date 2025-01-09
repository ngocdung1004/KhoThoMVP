using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class JobTypeService : IJobTypeService
    {
        private readonly DungnnExe201Thodung5Context _context;
        private readonly IMapper _mapper;

        public JobTypeService(DungnnExe201Thodung5Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobTypeDto>> GetAllJobTypesAsync()
        {
            var jobTypes = await _context.JobTypes.ToListAsync();
            return _mapper.Map<IEnumerable<JobTypeDto>>(jobTypes);
        }

        public async Task<JobTypeDto> GetJobTypeByIdAsync(int id)
        {
            var jobType = await _context.JobTypes.FindAsync(id);
            return _mapper.Map<JobTypeDto>(jobType);
        }

        public async Task<JobTypeDto> CreateJobTypeAsync(JobTypeDto jobTypeDto)
        {
            var jobType = _mapper.Map<JobType>(jobTypeDto);
            _context.JobTypes.Add(jobType);
            await _context.SaveChangesAsync();
            return _mapper.Map<JobTypeDto>(jobType);
        }

        public async Task<JobTypeDto> UpdateJobTypeAsync(int id, JobTypeDto jobTypeDto)
        {
            var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType == null) return null;

            _mapper.Map(jobTypeDto, jobType);
            await _context.SaveChangesAsync();
            return _mapper.Map<JobTypeDto>(jobType);
        }

        public async Task DeleteJobTypeAsync(int id)
        {
            var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType != null)
            {
                _context.JobTypes.Remove(jobType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
