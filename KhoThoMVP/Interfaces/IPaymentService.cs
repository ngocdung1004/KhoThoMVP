using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto> GetPaymentByIdAsync(int id);
        Task<IEnumerable<PaymentDto>> GetPaymentsByWorkerIdAsync(int workerId);
        Task<PaymentDto> CreatePaymentAsync(PaymentDto paymentDto);
        Task<PaymentDto> UpdatePaymentAsync(int id, PaymentDto paymentDto);
        Task DeletePaymentAsync(int id);
    }
}
