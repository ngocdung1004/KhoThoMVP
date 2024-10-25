using KhoThoMVP.DTOs;

namespace KhoThoMVP.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewDto>> GetReviewsByWorkerIdAsync(int workerId);
        Task<IEnumerable<ReviewDto>> GetReviewsByCustomerIdAsync(int customerId);
        Task<ReviewDto> CreateReviewAsync(ReviewDto reviewDto);
        Task<ReviewDto> UpdateReviewAsync(int id, ReviewDto reviewDto);
        Task DeleteReviewAsync(int id);
        Task<double> GetAverageRatingForWorkerAsync(int workerId);
    }
}
