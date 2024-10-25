using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int WorkerId { get; set; }

    public int CustomerId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
