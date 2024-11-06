using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingDto>> GetBookingsByCustomerIdAsync(int customerId);
        Task<IEnumerable<BookingDto>> GetBookingsByWorkerIdAsync(int workerId);
        Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
        Task<BookingDto> UpdateBookingStatusAsync(int id, string status);
        Task<bool> DeleteBookingAsync(int id);
    }
}
