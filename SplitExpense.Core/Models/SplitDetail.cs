using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SplitExpense.Infra;

[PrimaryKey("GroupId", "ExpenseId", "UserId")]
[Table("split_details")]
public partial class SplitDetail
{
    [Key]
    [Column("group_id")]
    public Guid GroupId { get; set; }

    [Key]
    [Column("expense_id")]
    public Guid ExpenseId { get; set; }

    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("percentage", TypeName = "decimal(5, 2)")]
    public decimal? Percentage { get; set; }

    [Column("split_amount", TypeName = "decimal(18, 0)")]
    public decimal? SplitAmount { get; set; }

    [ForeignKey("ExpenseId")]
    [InverseProperty("SplitDetails")]
    public virtual Expense Expense { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("SplitDetails")]
    public virtual Group Group { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("SplitDetails")]
    public virtual User User { get; set; } = null!;
}
