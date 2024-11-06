using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IWorkerRateService
    {
        Task<IEnumerable<WorkerRateDto>> GetAllRatesAsync();
        Task<WorkerRateDto> GetRateByIdAsync(int id);
        Task<IEnumerable<WorkerRateDto>> GetRatesByWorkerIdAsync(int workerId);
        Task<WorkerRateDto> CreateRateAsync(CreateWorkerRateDto dto);
        Task<WorkerRateDto> UpdateRateAsync(int id, CreateWorkerRateDto dto);
        Task<bool> DeleteRateAsync(int id);
    }
}
