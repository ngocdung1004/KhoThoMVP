namespace KhoThoMVP.DTOs
{
    public class BookingPaymentDto
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
    }
}
