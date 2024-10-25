using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
        Task<SubscriptionDto> GetSubscriptionByIdAsync(int id);
        Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByWorkerIdAsync(int workerId);
        Task<SubscriptionDto> CreateSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task<SubscriptionDto> UpdateSubscriptionAsync(int id, SubscriptionDto subscriptionDto);
        Task DeleteSubscriptionAsync(int id);
    }
}