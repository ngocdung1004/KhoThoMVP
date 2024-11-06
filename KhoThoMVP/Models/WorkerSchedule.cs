using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class WorkerSchedule
{
    public int ScheduleId { get; set; }

    public int WorkerId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
