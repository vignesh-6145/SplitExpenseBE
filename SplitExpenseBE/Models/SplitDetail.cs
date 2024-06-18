using System;
using System.Collections.Generic;

namespace SplitExpenseBE.Models;

public partial class SplitDetail
{
    public Guid GroupId { get; set; }

    public Guid ExpenseId { get; set; }

    public Guid UserId { get; set; }

    public decimal? Percentage { get; set; }

    public decimal? SplitAmount { get; set; }

    public virtual Expense Expense { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
