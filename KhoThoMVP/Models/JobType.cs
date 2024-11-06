using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class JobType
{
    public int JobTypeId { get; set; }

    public string JobTypeName { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<WorkerJobType> WorkerJobTypes { get; set; } = new List<WorkerJobType>();

    public virtual ICollection<WorkerRate> WorkerRates { get; set; } = new List<WorkerRate>();
}
