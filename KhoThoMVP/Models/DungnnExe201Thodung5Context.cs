using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KhoThoMVP.Models;

public partial class DungnnExe201Thodung5Context : DbContext
{
    public DungnnExe201Thodung5Context()
    {
    }

    public DungnnExe201Thodung5Context(DbContextOptions<DungnnExe201Thodung5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingCancellation> BookingCancellations { get; set; }

    public virtual DbSet<BookingPayment> BookingPayments { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<WorkerJobType> WorkerJobTypes { get; set; }

    public virtual DbSet<WorkerRate> WorkerRates { get; set; }

    public virtual DbSet<WorkerSchedule> WorkerSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-SVMK9VQ;Initial Catalog=dungnn_exe201_thodung5;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE4E80AD03A51");

            entity.HasIndex(e => e.Username, "UQ__Admins__536C85E45DB2F5D2").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Admins__A9D10534F699460C").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD900848B4");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalHours).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Custom__6754599E");

            entity.HasOne(d => d.JobType).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__JobTyp__693CA210");

            entity.HasOne(d => d.Worker).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Worker__68487DD7");
        });

        modelBuilder.Entity<BookingCancellation>(entity =>
        {
            entity.HasKey(e => e.CancellationId).HasName("PK__BookingC__6A2D9A1A06D9334E");

            entity.Property(e => e.CancellationId).HasColumnName("CancellationID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CancelledAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RefundAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RefundStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingCancellations)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingCa__Booki__778AC167");

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.BookingCancellations)
                .HasForeignKey(d => d.CancelledBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingCa__Cance__787EE5A0");
        });

        modelBuilder.Entity<BookingPayment>(entity =>
        {
            entity.HasKey(e => e.BookingPaymentId).HasName("PK__BookingP__B9EFED93BA9736E0");

            entity.Property(e => e.BookingPaymentId).HasColumnName("BookingPaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.PaymentTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingPayments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingPa__Booki__72C60C4A");
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.JobTypeId).HasName("PK__JobTypes__E1F4624DB0B3881E");

            entity.HasIndex(e => e.JobTypeName, "UQ__JobTypes__2C951EA84E6132E1").IsUnique();

            entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");
            entity.Property(e => e.JobTypeName).HasMaxLength(255);
        });

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__Password__658FEE8AC1C02751");

            entity.Property(e => e.TokenId).HasColumnName("TokenID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.Token).HasMaxLength(6);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PasswordResetTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PasswordR__UserI__5DCAEF64");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A5834D9A778");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaidAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.Worker).WithMany(p => p.Payments)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Worker__59063A47");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEB313AFE0");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Custome__5441852A");

            entity.HasOne(d => d.Worker).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__WorkerI__534D60F1");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B24BDA21EFAA4");

            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Qrcode)
                .HasMaxLength(255)
                .HasColumnName("QRCode");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.SubscriptionType).HasMaxLength(50);
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.Worker).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__Worke__4E88ABD4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE3868572");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053438FAEE0C").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ProfilePicture).HasMaxLength(255);
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.WorkerId).HasName("PK__Workers__077C880680289B02");

            entity.HasIndex(e => e.UserId, "UQ__Workers__1788CCAD10F6184F").IsUnique();

            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");
            entity.Property(e => e.BackIdcard)
                .HasMaxLength(255)
                .HasColumnName("BackIDCard");
            entity.Property(e => e.FrontIdcard)
                .HasMaxLength(255)
                .HasColumnName("FrontIDCard");
            entity.Property(e => e.ProfileImage).HasMaxLength(255);
            entity.Property(e => e.Rating).HasDefaultValue(0.0);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Verified).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithOne(p => p.Worker)
                .HasForeignKey<Worker>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Workers__UserID__45F365D3");
        });

        modelBuilder.Entity<WorkerJobType>(entity =>
        {
            entity.HasKey(e => e.WorkerJobTypeId).HasName("PK__WorkerJo__1D437F13D5A39938");

            entity.HasIndex(e => new { e.WorkerId, e.JobTypeId }, "UC_WorkerJob").IsUnique();

            entity.Property(e => e.WorkerJobTypeId).HasColumnName("WorkerJobTypeID");
            entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.JobType).WithMany(p => p.WorkerJobTypes)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkerJob__JobTy__4AB81AF0");

            entity.HasOne(d => d.Worker).WithMany(p => p.WorkerJobTypes)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkerJob__Worke__49C3F6B7");
        });

        modelBuilder.Entity<WorkerRate>(entity =>
        {
            entity.HasKey(e => e.RateId).HasName("PK__WorkerRa__58A7CCBCB38EF5F6");

            entity.HasIndex(e => new { e.WorkerId, e.JobTypeId }, "UC_WorkerRate").IsUnique();

            entity.Property(e => e.RateId).HasColumnName("RateID");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.JobTypeId).HasColumnName("JobTypeID");
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.JobType).WithMany(p => p.WorkerRates)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkerRat__JobTy__6E01572D");

            entity.HasOne(d => d.Worker).WithMany(p => p.WorkerRates)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkerRat__Worke__6D0D32F4");
        });

        modelBuilder.Entity<WorkerSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__WorkerSc__9C8A5B69EF54D97E");

            entity.HasIndex(e => new { e.WorkerId, e.DayOfWeek }, "UC_WorkerSchedule").IsUnique();

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.WorkerId).HasColumnName("WorkerID");

            entity.HasOne(d => d.Worker).WithMany(p => p.WorkerSchedules)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkerSch__Worke__628FA481");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
