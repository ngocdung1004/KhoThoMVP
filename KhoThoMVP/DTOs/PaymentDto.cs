namespace KhoThoMVP.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int WorkerId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? PaymentStatus { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
