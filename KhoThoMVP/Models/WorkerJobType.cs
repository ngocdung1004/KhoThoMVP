using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class WorkerJobType
{
    public int WorkerJobTypeId { get; set; }

    public int WorkerId { get; set; }

    public int JobTypeId { get; set; }

    public virtual JobType JobType { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
