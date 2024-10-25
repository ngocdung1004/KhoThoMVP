using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IWorkerService
    {
        Task<IEnumerable<WorkerDto>> GetAllWorkersAsync();
        Task<WorkerDto> GetWorkerByIdAsync(int id);
        Task<WorkerDto> CreateWorkerAsync(WorkerDto workerDto);
        Task<WorkerDto> UpdateWorkerAsync(int id, WorkerDto workerDto);
        Task DeleteWorkerAsync(int id);
        Task<IEnumerable<WorkerDto>> GetWorkersByJobTypeAsync(int jobTypeId);
    }
}
