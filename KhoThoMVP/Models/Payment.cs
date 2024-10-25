using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int WorkerId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? PaymentStatus { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
