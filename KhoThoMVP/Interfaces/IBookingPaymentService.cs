using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IBookingPaymentService
    {
        Task<IEnumerable<BookingPaymentDto>> GetAllPaymentsAsync();
        Task<BookingPaymentDto> GetPaymentByIdAsync(int id);
        Task<BookingPaymentDto> GetPaymentByBookingIdAsync(int bookingId);
        Task<BookingPaymentDto> CreatePaymentAsync(CreateBookingPaymentDto dto);
        Task<BookingPaymentDto> UpdatePaymentStatusAsync(int id, string status);
        Task<BookingPaymentDto> UpdateTransferredToWorker(int id, bool trans);
    }
}
