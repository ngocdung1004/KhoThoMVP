using AutoMapper;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Services
{
    public class BookingPaymentService : IBookingPaymentService
    {
        private readonly DungnnExe201Thodung5Context _context;
        private readonly IMapper _mapper;

        public BookingPaymentService(DungnnExe201Thodung5Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingPaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _context.BookingPayments.ToListAsync();
            return _mapper.Map<IEnumerable<BookingPaymentDto>>(payments);
        }

        public async Task<BookingPaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.BookingPayments
                .FirstOrDefaultAsync(p => p.BookingPaymentId == id);
            return _mapper.Map<BookingPaymentDto>(payment);
        }

        public async Task<BookingPaymentDto> GetPaymentByBookingIdAsync(int bookingId)
        {
            var payment = await _context.BookingPayments
                .FirstOrDefaultAsync(p => p.BookingId == bookingId);
            return _mapper.Map<BookingPaymentDto>(payment);
        }

        public async Task<BookingPaymentDto> CreatePaymentAsync(CreateBookingPaymentDto dto)
        {
            var payment = _mapper.Map<BookingPayment>(dto);
            payment.PaymentStatus = "Pending";
            payment.PaymentTime = DateTime.UtcNow;
            payment.WorkerAmount = payment.Amount * (payment.CommissionRate / 100);
            payment.PlatformAmount = payment.Amount * ((100 - payment.CommissionRate) / 100);
            payment.TransferredToWorker = false;

            _context.BookingPayments.Add(payment);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingPaymentDto>(payment);
        }

        public async Task<BookingPaymentDto> UpdatePaymentStatusAsync(int id, string status)
        {
            var payment = await _context.BookingPayments.FindAsync(id);
            if (payment == null) return null;

            payment.PaymentStatus = status;
            if (status == "Completed")
            {
                payment.PaymentTime = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<BookingPaymentDto>(payment);
        }
    }
}

