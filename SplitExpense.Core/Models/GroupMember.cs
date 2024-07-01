using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SplitExpense.Infra;

[PrimaryKey("GroupId", "UserId")]
[Table("group_members")]
public partial class GroupMember
{
    [Key]
    [Column("group_id")]
    public Guid GroupId { get; set; }

    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("doj")]
    public DateOnly? Doj { get; set; }

    [Column("dol")]
    public DateOnly? Dol { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("GroupMembers")]
    public virtual Group Group { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("GroupMembers")]
    public virtual User User { get; set; } = null!;
}
