namespace KhoThoMVP.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int UserType { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
