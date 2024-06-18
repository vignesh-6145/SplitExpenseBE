using System;
using System.Collections.Generic;

namespace SplitExpenseBE.Models;

public partial class Group
{
    public Guid GroupId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateOnly? Doc { get; set; }

    public Guid? AdminId { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? Admin { get; set; }

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<SplitDetail> SplitDetails { get; set; } = new List<SplitDetail>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
