namespace KhoThoMVP.DTOs
{
    public class BookingDto
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int WorkerID { get; set; }
        public int JobTypeID { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal TotalHours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
