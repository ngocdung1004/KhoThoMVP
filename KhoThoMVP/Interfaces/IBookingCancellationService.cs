using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IBookingCancellationService
    {
        Task<IEnumerable<BookingCancellationDto>> GetAllCancellationsAsync();
        Task<BookingCancellationDto> GetCancellationByIdAsync(int id);
        Task<BookingCancellationDto> GetCancellationByBookingIdAsync(int bookingId);
        Task<BookingCancellationDto> CreateCancellationAsync(CreateBookingCancellationDto dto);
        Task<BookingCancellationDto> UpdateRefundStatusAsync(int id, string status);
    }
}
