namespace KhoThoMVP.DTOs
{
    public class BookingCancellationDto
    {
        public int CancellationID { get; set; }
        public int BookingID { get; set; }
        public int CancelledBy { get; set; }
        public string CancellationReason { get; set; }
        public DateTime CancelledAt { get; set; }
        public decimal RefundAmount { get; set; }
        public string RefundStatus { get; set; }
    }
}
