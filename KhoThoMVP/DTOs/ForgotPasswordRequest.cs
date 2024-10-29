using System.ComponentModel.DataAnnotations;

namespace KhoThoMVP.DTOs
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
