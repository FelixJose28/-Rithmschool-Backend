using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rithmschool.Core.Entities;

#nullable disable

namespace Rithmschool.Infrastructure.Data
{
    public partial class RithmschoolContext : DbContext
    {
        public RithmschoolContext()
        {
        }

        public RithmschoolContext(DbContextOptions<RithmschoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buy> Buys { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Buy>(entity =>
            {
                entity.ToTable("Buy");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Buys)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseIdBuy");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Buys)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserIdBuy");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Route)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_TeacherIdCourse");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Skills)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
