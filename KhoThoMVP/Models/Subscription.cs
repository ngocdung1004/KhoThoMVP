using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int WorkerId { get; set; }

    public string SubscriptionType { get; set; } = null!;

    public string? PaymentStatus { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Qrcode { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
