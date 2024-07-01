using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=SplitExpense;Integrated Security=True;TrustServerCertificate=True;");
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__expenses__404B6A6B4C76D59C");

            entity.Property(e => e.ExpenseId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.Expenses).HasConstraintName("fk_user_expense");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__groups__D57795A0D10A6FCE");

            entity.Property(e => e.GroupId).ValueGeneratedNever();

            entity.HasOne(d => d.Admin).WithMany(p => p.Groups).HasConstraintName("fk_group_admin");

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

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_group_members");

            entity.HasOne(d => d.User).WithMany(p => p.GroupMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_group_members");
        });

        modelBuilder.Entity<SplitDetail>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.ExpenseId, e.UserId }).HasName("pk_split_details");

            entity.HasOne(d => d.Expense).WithMany(p => p.SplitDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_expense_split_details");

            entity.HasOne(d => d.Group).WithMany(p => p.SplitDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_group_split_details");

            entity.HasOne(d => d.User).WithMany(p => p.SplitDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_split_details");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F7A432609");

            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
