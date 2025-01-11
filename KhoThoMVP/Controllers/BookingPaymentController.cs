using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingPaymentController : ControllerBase
    {
        private readonly IBookingPaymentService _paymentService;

        public BookingPaymentController(IBookingPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize(Roles = "0,1,2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingPaymentDto>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }
        [Authorize(Roles = "0,1,2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingPaymentDto>> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
        [Authorize(Roles = "0,1,2")]
        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<BookingPaymentDto>> GetPaymentByBookingId(int bookingId)
        {
            var payment = await _paymentService.GetPaymentByBookingIdAsync(bookingId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
        [Authorize(Roles = "0,1,2")]
        [HttpPost]
        public async Task<ActionResult<BookingPaymentDto>> CreatePayment(CreateBookingPaymentDto dto)
        {
            var payment = await _paymentService.CreatePaymentAsync(dto);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.BookingPaymentId }, payment);
        }
        [Authorize(Roles = "0,1,2")]
        [HttpPut("{id}/status")]
        public async Task<ActionResult<BookingPaymentDto>> UpdatePaymentStatus(int id, [FromBody] string status)
        {
            var payment = await _paymentService.UpdatePaymentStatusAsync(id, status);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
    }
}
