using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SplitExpense.Infra;

namespace SplitExpense.Infra.Data;

public partial class SplitExpenseContext : DbContext
{
    public SplitExpenseContext()
    {
    }

    public SplitExpenseContext(DbContextOptions<SplitExpenseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<SplitDetail> SplitDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=SplitExpense;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__expenses__404B6A6B4C76D59C");

            entity.ToTable("expenses");

            entity.Property(e => e.ExpenseId)
                .ValueGeneratedNever()
                .HasColumnName("expense_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.Doc).HasColumnName("doc");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Settled).HasColumnName("settled");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_expense");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__groups__D57795A0D10A6FCE");

            entity.ToTable("groups");

            entity.Property(e => e.GroupId)
                .ValueGeneratedNever()
                .HasColumnName("group_id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Doc).HasColumnName("doc");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Admin).WithMany(p => p.Groups)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("fk_group_admin");

            entity.HasMany(d => d.Expenses).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupExpense",
                    r => r.HasOne<Expense>().WithMany()
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_expense_group_expense"),
                    l => l.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_group_group_expense"),
                    j =>
                    {
                        j.HasKey("GroupId", "ExpenseId").HasName("pk_group_expense");
                        j.ToTable("group_expense");
                        j.IndexerProperty<Guid>("GroupId").HasColumnName("group_id");
                        j.IndexerProperty<Guid>("ExpenseId").HasColumnName("expense_id");
                    });
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.UserId }).HasName("pk_group_members");

            entity.ToTable("group_members");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Doj).HasColumnName("doj");
            entity.Property(e => e.Dol).HasColumnName("dol");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_group_members");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_group_members");
        });

        modelBuilder.Entity<SplitDetail>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.ExpenseId, e.UserId }).HasName("pk_split_details");

            entity.ToTable("split_details");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.ExpenseId).HasColumnName("expense_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Percentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.SplitAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("split_amount");

            entity.HasOne(d => d.Expense).WithMany(p => p.SplitDetails)
                .HasForeignKey(d => d.ExpenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_expense_split_details");

            entity.HasOne(d => d.Group).WithMany(p => p.SplitDetails)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_split_details");

            entity.HasOne(d => d.User).WithMany(p => p.SplitDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_split_details");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F7A432609");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164E954F349").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Doj).HasColumnName("doj");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
