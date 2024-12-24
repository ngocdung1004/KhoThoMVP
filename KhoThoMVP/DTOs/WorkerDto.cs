namespace KhoThoMVP.DTOs
{
    public class WorkerDto
    {
        public int WorkerId { get; set; }
        public int UserId { get; set; }
        public int ExperienceYears { get; set; }
        public float? Rating { get; set; }
        public string? Bio { get; set; }
        public bool? Verified { get; set; }
        public UserDto? User { get; set; }
        public string? ProfileImage { get; set; }
        public string? FrontIdcard { get; set; }
        public string? BackIdcard { get; set; }
    }
}
