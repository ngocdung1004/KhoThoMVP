namespace KhoThoMVP.DTOs
{
    public class CreateBookingDto
    {
        public int CustomerID { get; set; }
        public int WorkerID { get; set; }
        public int JobTypeID { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
    }
}
