using Microsoft.AspNetCore.Mvc;
using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;

namespace KhoThoMVP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            try
            {
                var review = await _reviewService.GetReviewByIdAsync(id);
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("worker/{workerId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByWorker(int workerId)
        {
            var reviews = await _reviewService.GetReviewsByWorkerIdAsync(workerId);
            return Ok(reviews);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByCustomer(int customerId)
        {
            var reviews = await _reviewService.GetReviewsByCustomerIdAsync(customerId);
            return Ok(reviews);
        }

        [HttpGet("worker/{workerId}/rating")]
        public async Task<ActionResult<double>> GetWorkerAverageRating(int workerId)
        {
            var rating = await _reviewService.GetAverageRatingForWorkerAsync(workerId);
            return Ok(rating);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview(ReviewDto reviewDto)
        {
            try
            {
                var createdReview = await _reviewService.CreateReviewAsync(reviewDto);
                return CreatedAtAction(nameof(GetReview), new { id = createdReview.ReviewId }, createdReview);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewDto>> UpdateReview(int id, ReviewDto reviewDto)
        {
            try
            {
                var updatedReview = await _reviewService.UpdateReviewAsync(id, reviewDto);
                return Ok(updatedReview);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}