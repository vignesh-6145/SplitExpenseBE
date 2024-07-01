using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SplitExpense.Infra;

[Table("expenses")]
public partial class Expense
{
    [Key]
    [Column("expense_id")]
    public Guid ExpenseId { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("amount", TypeName = "decimal(18, 0)")]
    public decimal? Amount { get; set; }

    [Column("doc")]
    public DateOnly? Doc { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("settled")]
    public bool? Settled { get; set; }

    [InverseProperty("Expense")]
    public virtual ICollection<SplitDetail> SplitDetails { get; set; } = new List<SplitDetail>();

    [ForeignKey("UserId")]
    [InverseProperty("Expenses")]
    public virtual User? User { get; set; }

    [ForeignKey("ExpenseId")]
    [InverseProperty("Expenses")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
