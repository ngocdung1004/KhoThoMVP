// PaymentService.cs
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
        private readonly IWebHostEnvironment _environment;

        public PaymentService(KhoThoContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments
                .Include(p => p.Worker)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Worker)
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByWorkerIdAsync(int workerId)
        {
            var payments = await _context.Payments
                .Include(p => p.Worker)
                .Where(p => p.WorkerId == workerId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        private async Task<string> SavePaymentImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "payments");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"/uploads/payments/{fileName}";
        }

        public async Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var payment = new Payment
            {
                WorkerId = createPaymentDto.WorkerId,
                Amount = createPaymentDto.Amount,
                PaymentMethod = createPaymentDto.PaymentMethod,
                PaymentStatus = createPaymentDto.PaymentStatus,
                PaidAt = createPaymentDto.PaidAt
            };

            if (createPaymentDto.PaymentImage != null)
            {
                payment.PaymentImage = await SavePaymentImageAsync(createPaymentDto.PaymentImage);
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> UpdatePaymentAsync(int id, PaymentDto paymentDto)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            _mapper.Map(paymentDto, payment);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found");

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}