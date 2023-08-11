using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Models
{
    public partial class OrganizeMeFinalDBContext : DbContext
    {
        public OrganizeMeFinalDBContext()
        {
        }

        public OrganizeMeFinalDBContext(DbContextOptions<OrganizeMeFinalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskCategory> TaskCategories { get; set; } = null!;
        public virtual DbSet<TaskList> TaskLists { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        [DbFunction("ufn_GenerateNewTaskListId", "dbo")]
        public static byte GenerateNewTaskListId()
        {
            return 0;
        }
        [DbFunction("ufn_GenerateNewTaskCategoryId", "dbo")]
        public static byte GenerateNewTaskCategoryId()
        {
            return 0;
        }
        [DbFunction("ufn_GenerateNewTaskId", "dbo")]
        public static string GenerateNewTaskId()
        {
            return "";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=IDRSUC606658L;Database=OrganizeMeFinalDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.TaskId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AssignedTo).HasColumnType("numeric(7, 0)");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.EmpId).HasColumnType("numeric(7, 0)");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskNote)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.TaskAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .HasConstraintName("fk_AssignedTo");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.TaskEmps)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("fk_EmpId");

                entity.HasOne(d => d.TaskCategory)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskCategoryId)
                    .HasConstraintName("fk_TaskCategoryId");

                entity.HasOne(d => d.TaskList)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskListId)
                    .HasConstraintName("fk_TaskListId");
            });

            modelBuilder.Entity<TaskCategory>(entity =>
            {
                entity.ToTable("TaskCategory");

                entity.Property(e => e.TaskCategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaskList>(entity =>
            {
                entity.ToTable("TaskList");

                entity.Property(e => e.TaskListName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("pk_EmpId");

                entity.Property(e => e.EmpId).HasColumnType("numeric(7, 0)");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
