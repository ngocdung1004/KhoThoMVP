namespace KhoThoMVP.DTOs
{
    public class WorkerScheduleDto
    {
        public int ScheduleID { get; set; }
        public int WorkerID { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}
