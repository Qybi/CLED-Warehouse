using System;
using System.Collections.Generic;
using CLED.Warehouse.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace CLED.Warehouse.Web;

public partial class WarehouseContext : DbContext
{
    public WarehouseContext()
    {
    }

    public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessoriesAssignment> AccessoriesAssignments { get; set; }

    public virtual DbSet<Accessory> Accessories { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Pc> Pcs { get; set; }

    public virtual DbSet<PcAssignment> Pcassignments { get; set; }

    public virtual DbSet<PcModelStock> PcmodelStocks { get; set; }

    public virtual DbSet<ReasonsAssignment> ReasonsAssignments { get; set; }

    public virtual DbSet<ReasonsReturn> ReasonsReturns { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=Vmware1!;Host=localhost;Port=5432;Database=Warehouse;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessoriesAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccessoriesAssignments_pkey");

            entity.Property(e => e.ActualReturnDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AssignmentDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ForecastedReturnDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsReturned).HasDefaultValue(false);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Accessory).WithMany(p => p.AccessoriesAssignments)
                .HasForeignKey(d => d.AccessoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessoriesAssignments_AccessoryId");

            entity.HasOne(d => d.AssignmentReason).WithMany(p => p.AccessoriesAssignments)
                .HasForeignKey(d => d.AssignmentReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessoriesAssignments_AssignmentReasonId");

            entity.HasOne(d => d.ReturnReason).WithMany(p => p.AccessoriesAssignments)
                .HasForeignKey(d => d.ReturnReasonId)
                .HasConstraintName("FK_AccessoriesAssignments_ReturnReasonId");

            entity.HasOne(d => d.Student).WithMany(p => p.AccessoriesAssignments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessoriesAssignments_StudentId");
        });

        modelBuilder.Entity<Accessory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Accessories_pkey");

            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Stock).WithMany(p => p.Accessories)
                .HasForeignKey(d => d.StockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accessories_PCStockId");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Courses_pkey");

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.DateEnd).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DateStart).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ShortName).HasMaxLength(30);
            entity.Property(e => e.Status).HasMaxLength(255);
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pcs_pkey");

            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsMuletto).HasDefaultValue(false);
            entity.Property(e => e.PropertySticker).HasMaxLength(255);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Serial).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.Stock).WithMany(p => p.Pcs)
                .HasForeignKey(d => d.StockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PCS_StockId");
        });

        modelBuilder.Entity<PcAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PCAssignments_pkey");

            entity.ToTable("PCAssignments");

            entity.Property(e => e.ActualReturnDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.AssignmentDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ForecastedReturnDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsReturned).HasDefaultValue(false);
            entity.Property(e => e.PcId).HasColumnName("PCId");
            entity.Property(e => e.PropertySticker)
                .HasMaxLength(255)
                .HasColumnName("propertysticker");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.AssignmentReason).WithMany(p => p.Pcassignments)
                .HasForeignKey(d => d.AssignmentReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PCASsignments_AssignmentReasonId");

            entity.HasOne(d => d.Pc).WithMany(p => p.Pcassignments)
                .HasForeignKey(d => d.PcId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PCASsignments_PCId");

            entity.HasOne(d => d.ReturnReason).WithMany(p => p.Pcassignments)
                .HasForeignKey(d => d.ReturnReasonId)
                .HasConstraintName("FK_PCASsignments_ReturnReasonId");

            entity.HasOne(d => d.Student).WithMany(p => p.PcAssignments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PCASsignments_StudentId");
        });

        modelBuilder.Entity<PcModelStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PCModelStock_pkey");

            entity.ToTable("PCModelStock");

            entity.Property(e => e.Brand).HasMaxLength(255);
            entity.Property(e => e.Cpu)
                .HasMaxLength(255)
                .HasColumnName("CPU");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Model).HasMaxLength(255);
            entity.Property(e => e.PurchaseDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Ram)
                .HasMaxLength(255)
                .HasColumnName("RAM");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Storage).HasMaxLength(255);
            entity.Property(e => e.TotalQuantity).HasDefaultValue(0);
        });

        modelBuilder.Entity<ReasonsAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReasonsAssignment_pkey");

            entity.ToTable("ReasonsAssignment");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<ReasonsReturn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReasonsReturn_pkey");

            entity.ToTable("ReasonsReturn");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Students_pkey");

            entity.Property(e => e.BirthCity).HasMaxLength(150);
            entity.Property(e => e.BirthNation).HasMaxLength(150);
            entity.Property(e => e.BirthProvince).HasMaxLength(150);
            entity.Property(e => e.CourseStartAge).HasPrecision(9, 1);
            entity.Property(e => e.EmailPersonal).HasMaxLength(255);
            entity.Property(e => e.EmailUser).HasMaxLength(255);
            entity.Property(e => e.ExamHonors).HasMaxLength(255);
            entity.Property(e => e.ExamOutcome).HasMaxLength(100);
            entity.Property(e => e.ExamScore).HasPrecision(9, 2);
            entity.Property(e => e.FiscalCode).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(25);
            entity.Property(e => e.IalmanId)
                .HasMaxLength(255)
                .HasColumnName("IALManID");
            entity.Property(e => e.IsInInternship).HasDefaultValue(false);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.ProfessionalStatus).HasMaxLength(512);
            entity.Property(e => e.ResidenceCity).HasMaxLength(150);
            entity.Property(e => e.ResidenceNation).HasMaxLength(150);
            entity.Property(e => e.ResidenceProvince).HasMaxLength(150);
            entity.Property(e => e.SchoolDegree).HasMaxLength(300);
            entity.Property(e => e.SchoolDegreeTitle).HasMaxLength(255);
            entity.Property(e => e.SchoolIdentifierId)
                .HasMaxLength(255)
                .HasColumnName("SchoolIdentifierID");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_CourseId");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_UserId");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tickets_pkey");

            entity.Property(e => e.DateClose).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DateOpen)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.TicketBody).HasMaxLength(1024);
            entity.Property(e => e.TicketType).HasMaxLength(255);
            entity.Property(e => e.UserClaimClose).HasMaxLength(255);
            entity.Property(e => e.UserClaimOpen).HasMaxLength(255);

            entity.HasOne(d => d.Student).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_StudentId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.DeletedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Enabled).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(1024);
            entity.Property(e => e.PasswordSalt).HasMaxLength(512);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Roles).HasColumnType("character varying[]");
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
