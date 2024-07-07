using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SplitExpense.Infra;

[Table("users")]
[Index("Email", Name = "UQ__users__AB6E6164E954F349", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("user_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [Column("email")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("dob")]
    public DateOnly? Dob { get; set; }

    [Column("doj")]
    public DateOnly? Doj { get; set; }

    [Column("password")]
    [StringLength(25)]
    [Unicode(false)]
    [JsonIgnore]
    public string? Password { get; set; }

    [Column("is_active")]
    [JsonIgnore]
    public bool? IsActive { get; set; }

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    [InverseProperty("Admin")]
    [JsonIgnore]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [InverseProperty("User")]
    [JsonIgnore]
    public virtual ICollection<SplitDetail> SplitDetails { get; set; } = new List<SplitDetail>();
}
