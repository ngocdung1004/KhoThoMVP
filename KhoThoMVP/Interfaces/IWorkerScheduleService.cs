using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IWorkerScheduleService
    {
        Task<IEnumerable<WorkerScheduleDto>> GetAllSchedulesAsync();
        Task<WorkerScheduleDto> GetScheduleByIdAsync(int id);
        Task<IEnumerable<WorkerScheduleDto>> GetSchedulesByWorkerIdAsync(int workerId);
        Task<WorkerScheduleDto> CreateScheduleAsync(CreateWorkerScheduleDto dto);
        Task<WorkerScheduleDto> UpdateScheduleAsync(int id, CreateWorkerScheduleDto dto);
        Task<bool> DeleteScheduleAsync(int id);
    }
}
