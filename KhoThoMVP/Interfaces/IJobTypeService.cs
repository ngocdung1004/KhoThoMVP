using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IJobTypeService
    {
        Task<IEnumerable<JobTypeDto>> GetAllJobTypesAsync();
        Task<JobTypeDto> GetJobTypeByIdAsync(int id);
        Task<JobTypeDto> CreateJobTypeAsync(JobTypeDto jobTypeDto);
        Task<JobTypeDto> UpdateJobTypeAsync(int id, JobTypeDto jobTypeDto);
        Task DeleteJobTypeAsync(int id);
    }
}
