namespace KhoThoMVP.DTOs
{
    public class CreatePaymentDto
    {
        public int WorkerId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? PaymentStatus { get; set; }
        public DateTime PaidAt { get; set; }
        public IFormFile? PaymentImage { get; set; }
    }
}
