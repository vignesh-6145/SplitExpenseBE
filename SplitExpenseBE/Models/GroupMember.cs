using System;
using System.Collections.Generic;

namespace SplitExpenseBE.Models;

public partial class GroupMember
{
    public Guid GroupId { get; set; }

    public Guid UserId { get; set; }

    public DateOnly? Doj { get; set; }

    public DateOnly? Dol { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
