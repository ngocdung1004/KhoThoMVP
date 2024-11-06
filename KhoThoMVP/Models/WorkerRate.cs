using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class WorkerRate
{
    public int RateId { get; set; }

    public int WorkerId { get; set; }

    public int JobTypeId { get; set; }

    public decimal HourlyRate { get; set; }

    public virtual JobType JobType { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
