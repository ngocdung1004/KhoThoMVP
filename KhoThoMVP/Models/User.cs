using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int UserType { get; set; }

    public string? ProfilePicture { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BookingCancellation> BookingCancellations { get; set; } = new List<BookingCancellation>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Worker? Worker { get; set; }
}
