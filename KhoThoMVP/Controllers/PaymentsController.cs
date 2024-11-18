using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize(Roles = "0")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
        [Authorize(Roles = "0")]
        [HttpGet("worker/{workerId}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByWorkerId(int workerId)
        {
            var payments = await _paymentService.GetPaymentsByWorkerIdAsync(workerId);
            return Ok(payments);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(PaymentDto paymentDto)
        {
            var payment = await _paymentService.CreatePaymentAsync(paymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }
        [Authorize(Roles = "0, 1, 2")]
        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDto>> UpdatePayment(int id, PaymentDto paymentDto)
        {
            var payment = await _paymentService.UpdatePaymentAsync(id, paymentDto);
            if (payment == null) return NotFound();
            return Ok(payment);
        }
        [Authorize(Roles = "0")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}
