using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SplitExpense.Infra;

[Table("groups")]
public partial class Group
{
    [Key]
    [Column("group_id")]
    public Guid GroupId { get; set; }

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("doc")]
    public DateOnly? Doc { get; set; }

    [Column("admin_id")]
    public Guid? AdminId { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("Groups")]
    public virtual User? Admin { get; set; }

    [InverseProperty("Group")]
    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    [InverseProperty("Group")]
    public virtual ICollection<SplitDetail> SplitDetails { get; set; } = new List<SplitDetail>();

    [ForeignKey("GroupId")]
    [InverseProperty("Groups")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
