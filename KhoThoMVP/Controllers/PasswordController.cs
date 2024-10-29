using KhoThoMVP.DTOs;
using KhoThoMVP.Interfaces;
using KhoThoMVP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KhoThoMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly KhoThoContext _context;
        private readonly ILogger<PasswordController> _logger;
        public PasswordController(
            IConfiguration configuration,
            IEmailService emailService,
            KhoThoContext context,
            ILogger<PasswordController> logger)
        {
            _configuration = configuration;
            _emailService = emailService;
            _context = context;
            _logger = logger;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                _logger.LogInformation($"Forgot password request received for email: {request.Email}");

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    _logger.LogWarning($"User not found for email: {request.Email}");
                    return Ok();
                }

                var otp = new Random().Next(100000, 999999).ToString();
                _logger.LogInformation($"Generated OTP: {otp} for user: {user.UserId}");

                // Save OTP to database
                var passwordResetToken = new PasswordResetToken
                {
                    UserId = user.UserId,
                    Token = otp,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(15),
                    IsUsed = false
                };

                _context.PasswordResetTokens.Add(passwordResetToken);
                await _context.SaveChangesAsync();

                // Tạo email body
                var emailBody = $@"
                <h2>Yêu cầu đặt lại mật khẩu</h2>
                <p>Mã OTP của bạn là: <strong>{otp}</strong></p>
                <p>OTP này sẽ hết hạn sau 15 phút.</p>
            ";

                _logger.LogInformation($"About to send email to: {request.Email}");
                await _emailService.SendEmailAsync(
                    request.Email,
                    "Password Reset Request",
                    emailBody);
                _logger.LogInformation($"Email sent successfully to: {request.Email}");

                return Ok(new { message = "If your email exists in our system, you will receive a password reset OTP." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ForgotPassword: {ex.Message}");
                throw;
            }
        }

        [HttpGet("test-email")]
        public async Task<IActionResult> TestEmail()
        {
            try
            {
                await _emailService.SendEmailAsync(
                    "dungdangonthi@gmail.com",
                    "Test Email",
                    "<h1>This is a test email</h1>");
                return Ok("Test email sent successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid request" });

            var passwordResetToken = await _context.PasswordResetTokens
                .Where(t => t.UserId == user.UserId &&
                           t.Token == request.OTP &&
                           !t.IsUsed &&
                           t.ExpiryDate > DateTime.UtcNow)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();

            if (passwordResetToken == null)
                return BadRequest(new { message = "Invalid or expired OTP" });

            // Update password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            passwordResetToken.IsUsed = true;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Password has been reset successfully" });
        }
    }
}
