namespace KhoThoMVP.DTOs
{
    public class CreateBookingPaymentDto
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public decimal CommissionRate { get; set; }
    }
}
