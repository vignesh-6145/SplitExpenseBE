using System;
using System.Collections.Generic;

namespace SplitExpenseBE.Models;

public partial class Expense
{
    public Guid ExpenseId { get; set; }

    public string? Name { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? Doc { get; set; }

    public Guid? UserId { get; set; }

    public bool? Settled { get; set; }

    public virtual ICollection<SplitDetail> SplitDetails { get; set; } = new List<SplitDetail>();

    public virtual User? User { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
