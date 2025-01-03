﻿using System;
using System.Collections.Generic;

namespace KhoThoMVP.Models;

public partial class Worker
{
    public int WorkerId { get; set; }

    public int UserId { get; set; }

    public int ExperienceYears { get; set; }

    public double? Rating { get; set; }

    public string? Bio { get; set; }

    public bool? Verified { get; set; }

    public string? ProfileImage { get; set; }

    public string? FrontIdcard { get; set; }

    public string? BackIdcard { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WorkerJobType> WorkerJobTypes { get; set; } = new List<WorkerJobType>();

    public virtual ICollection<WorkerRate> WorkerRates { get; set; } = new List<WorkerRate>();

    public virtual ICollection<WorkerSchedule> WorkerSchedules { get; set; } = new List<WorkerSchedule>();
}
