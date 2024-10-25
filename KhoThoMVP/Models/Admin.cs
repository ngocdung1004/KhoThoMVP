using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }
}
