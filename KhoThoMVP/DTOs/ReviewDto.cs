namespace KhoThoMVP.DTOs
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int WorkerId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
