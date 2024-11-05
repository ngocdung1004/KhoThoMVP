namespace KhoThoMVP.DTOs
{
    public class CreateWorkerDto
    {
        public int UserId { get; set; }
        public int ExperienceYears { get; set; }
        public float? Rating { get; set; }
        public string? Bio { get; set; }
        public bool? Verified { get; set; }
    }
}
