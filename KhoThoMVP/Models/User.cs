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

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Worker? Worker { get; set; }
}
