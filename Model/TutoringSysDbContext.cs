using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TutoringSys_core.Model;

public partial class TutoringSysDbContext : DbContext
{
    public TutoringSysDbContext()
    {
        
    }

    public TutoringSysDbContext(DbContextOptions<TutoringSysDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AvailableSession> AvailableSessions { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<ReservedSession> ReservedSessions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<Tutoring> Tutorings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-AH0RDM8\\SQLEXPRESS;Initial Catalog=TutoringSysDb;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdId);

            entity.ToTable("Admin");

            entity.Property(e => e.AdId)
                .HasMaxLength(50)
                .HasColumnName("ad_Id");
            entity.Property(e => e.Fullnames)
                .HasMaxLength(50)
                .HasColumnName("fullnames");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        modelBuilder.Entity<AvailableSession>(entity =>
        {
            entity.ToTable("Available_sessions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .HasColumnName("course_code");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("date_time");
            entity.Property(e => e.TutorId)
                .HasMaxLength(50)
                .HasColumnName("tutor_id");

            entity.HasOne(d => d.CourseCodeNavigation).WithMany(p => p.AvailableSessions)
                .HasForeignKey(d => d.CourseCode)
                .HasConstraintName("FK_Available_sessions_Course");

            entity.HasOne(d => d.Tutor).WithMany(p => p.AvailableSessions)
                .HasForeignKey(d => d.TutorId)
                .HasConstraintName("FK_Available_sessions_Tutor");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Course");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Credit).HasColumnName("credit");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ReservedSession>(entity =>
        {
            entity.ToTable("Reserved_sessions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .HasColumnName("course_code");
            entity.Property(e => e.DateTime)
                .HasColumnType("datetime")
                .HasColumnName("date_time");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TutorId)
                .HasMaxLength(50)
                .HasColumnName("tutor_id");

            entity.HasOne(d => d.CourseCodeNavigation).WithMany(p => p.ReservedSessions)
                .HasForeignKey(d => d.CourseCode)
                .HasConstraintName("FK_Reserved_sessions_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.ReservedSessions)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Reserved_sessions_Student");

            entity.HasOne(d => d.Tutor).WithMany(p => p.ReservedSessions)
                .HasForeignKey(d => d.TutorId)
                .HasConstraintName("FK_Reserved_sessions_Tutor");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StId);

            entity.ToTable("Student");

            entity.Property(e => e.StId)
                .ValueGeneratedNever()
                .HasColumnName("st_Id");
            entity.Property(e => e.Fullnames)
                .HasMaxLength(50)
                .HasColumnName("fullnames");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.TrId);

            entity.ToTable("Tutor");

            entity.Property(e => e.TrId)
                .HasMaxLength(50)
                .HasColumnName("tr_Id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fullnames)
                .HasMaxLength(50)
                .HasColumnName("fullnames");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Tutoring>(entity =>
        {
            entity.ToTable("Tutoring");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .HasColumnName("course_code");
            entity.Property(e => e.TutorId)
                .HasMaxLength(50)
                .HasColumnName("tutor_id");

            entity.HasOne(d => d.CourseCodeNavigation).WithMany(p => p.Tutorings)
                .HasForeignKey(d => d.CourseCode)
                .HasConstraintName("FK_Tutoring_Course");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Tutorings)
                .HasForeignKey(d => d.TutorId)
                .HasConstraintName("FK_Tutoring_Tutor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
