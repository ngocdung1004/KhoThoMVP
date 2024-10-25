namespace KhoThoMVP.DTOs
{
    public class SubscriptionDto
    {
        public int SubscriptionId { get; set; }
        public int WorkerId { get; set; }
        public string SubscriptionType { get; set; } = null!;
        public string? PaymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? QrCode { get; set; }
    }
}
