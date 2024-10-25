using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly KhoThoContext _context;
        private readonly IMapper _mapper;

        public PaymentService(KhoThoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments.ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByWorkerIdAsync(int workerId)
        {
            var payments = await _context.Payments
                .Where(p => p.WorkerId == workerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> UpdatePaymentAsync(int id, PaymentDto paymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return null;

            _mapper.Map(paymentDto, payment);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
