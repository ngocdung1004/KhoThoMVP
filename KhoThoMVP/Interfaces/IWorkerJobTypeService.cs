using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IWorkerJobTypeService
    {
        Task<IEnumerable<WorkerJobTypeDto>> GetAllWorkerJobTypesAsync();
        Task<WorkerJobTypeDto> GetWorkerJobTypeByIdAsync(int id);
        Task<IEnumerable<WorkerJobTypeDto>> GetWorkerJobTypesByWorkerIdAsync(int workerId);
        Task<WorkerJobTypeDto> CreateWorkerJobTypeAsync(WorkerJobTypeDto workerJobTypeDto);
        Task<WorkerJobTypeDto> UpdateWorkerJobTypeAsync(int id, WorkerJobTypeDto workerJobTypeDto);
        Task DeleteWorkerJobTypeAsync(int id);
    }
}
