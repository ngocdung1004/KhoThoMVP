namespace KhoThoMVP.DTOs
{
    public class CreateBookingPaymentDto
    {
        public int BookingID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
