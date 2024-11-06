namespace KhoThoMVP.DTOs
{
    public class BookingPaymentDto
    {
        public int BookingPaymentID { get; set; }
        public int BookingID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionID { get; set; }
        public DateTime PaymentTime { get; set; }
    }
}
