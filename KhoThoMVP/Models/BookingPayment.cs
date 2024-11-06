using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class BookingPayment
{
    public int BookingPaymentId { get; set; }

    public int BookingId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? PaymentStatus { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? PaymentTime { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
