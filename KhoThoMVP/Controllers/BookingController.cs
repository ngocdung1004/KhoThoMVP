using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [Authorize(Roles = "0")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetByCustomerId(int customerId)
        {
            var bookings = await _bookingService.GetBookingsByCustomerIdAsync(customerId);
            return Ok(bookings);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("worker/{workerId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetByWorkerId(int workerId)
        {
            var bookings = await _bookingService.GetBookingsByWorkerIdAsync(workerId);
            return Ok(bookings);
        }
        [Authorize(Roles = "0, 1")]
        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create(CreateBookingDto dto)
        {
            var booking = await _bookingService.CreateBookingAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = booking.BookingID }, booking);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpPut("{id}/status")]
        public async Task<ActionResult<BookingDto>> UpdateStatus(int id, [FromBody] string status)
        {
            var booking = await _bookingService.UpdateBookingStatusAsync(id, status);
            if (booking == null) return NotFound();
            return Ok(booking);
        }
        [Authorize(Roles = "0")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
