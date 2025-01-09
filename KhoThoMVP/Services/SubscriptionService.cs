using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly DungnnExe201Thodung5Context _context;
        private readonly IMapper _mapper;

        public SubscriptionService(DungnnExe201Thodung5Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _context.Subscriptions
                .Include(s => s.Worker)
                .ToListAsync();
            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        public async Task<SubscriptionDto> GetSubscriptionByIdAsync(int id)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.Worker)
                .FirstOrDefaultAsync(s => s.SubscriptionId == id);

            if (subscription == null)
                throw new KeyNotFoundException($"Subscription with ID {id} not found");

            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsByWorkerIdAsync(int workerId)
        {
            var subscriptions = await _context.Subscriptions
                .Include(s => s.Worker)
                .Where(s => s.WorkerId == workerId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        }

        public async Task<SubscriptionDto> CreateSubscriptionAsync(SubscriptionDto subscriptionDto)
        {
            var subscription = _mapper.Map<Subscription>(subscriptionDto);

            // Set default values if needed
            subscription.StartDate = subscription.StartDate == default ? DateTime.UtcNow : subscription.StartDate;
            subscription.PaymentStatus = subscription.PaymentStatus ?? "Pending";

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task<SubscriptionDto> UpdateSubscriptionAsync(int id, SubscriptionDto subscriptionDto)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
                throw new KeyNotFoundException($"Subscription with ID {id} not found");

            _mapper.Map(subscriptionDto, subscription);
            await _context.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(subscription);
        }

        public async Task DeleteSubscriptionAsync(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
                throw new KeyNotFoundException($"Subscription with ID {id} not found");

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }
    }
}