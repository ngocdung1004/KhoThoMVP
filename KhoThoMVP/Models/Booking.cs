using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int CustomerId { get; set; }

    public int WorkerId { get; set; }

    public int JobTypeId { get; set; }

    public DateOnly BookingDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public decimal TotalHours { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BookingCancellation> BookingCancellations { get; set; } = new List<BookingCancellation>();

    public virtual ICollection<BookingPayment> BookingPayments { get; set; } = new List<BookingPayment>();

    public virtual User Customer { get; set; } = null!;

    public virtual JobType JobType { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
