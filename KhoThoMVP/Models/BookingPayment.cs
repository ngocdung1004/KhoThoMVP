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

    public decimal? WorkerAmount { get; set; }

    public decimal? PlatformAmount { get; set; }

    public decimal? CommissionRate { get; set; }

    public bool? TransferredToWorker { get; set; }

    public DateTime? TransferredToWorkerAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
