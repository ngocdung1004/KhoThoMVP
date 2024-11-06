using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class BookingCancellation
{
    public int CancellationId { get; set; }

    public int BookingId { get; set; }

    public int CancelledBy { get; set; }

    public string? CancellationReason { get; set; }

    public DateTime? CancelledAt { get; set; }

    public decimal? RefundAmount { get; set; }

    public string? RefundStatus { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User CancelledByNavigation { get; set; } = null!;
}
