using System;
using System.Collections.Generic;

namespace SplitExpenseBE.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public DateOnly? Dob { get; set; }

    public DateOnly? Doj { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<SplitDetail> SplitDetails { get; set; } = new List<SplitDetail>();
}
