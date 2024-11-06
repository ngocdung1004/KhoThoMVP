namespace KhoThoMVP.DTOs
{
    public class CreateBookingCancellationDto
    {
        public int BookingID { get; set; }
        public int CancelledBy { get; set; }
        public string CancellationReason { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
