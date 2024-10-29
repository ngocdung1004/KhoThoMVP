using System.ComponentModel.DataAnnotations;

namespace KhoThoMVP.DTOs
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OTP { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
