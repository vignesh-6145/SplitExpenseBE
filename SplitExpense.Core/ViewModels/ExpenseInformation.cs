using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.ViewModels
{
    public class ExpenseInformation
    {
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }

        public decimal? Amount { get; set; }

        public DateOnly? Doc { get; set; }

        public Guid? UserId { get; set; } //Id of the creator

        public bool? Settled { get; set; }

    }
}
