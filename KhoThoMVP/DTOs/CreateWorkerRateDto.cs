namespace KhoThoMVP.DTOs
{
    public class CreateWorkerRateDto
    {
        public int WorkerID { get; set; }
        public int JobTypeID { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
