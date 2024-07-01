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
    public class UserRegistration
    {
        [StringLength(50)]
        [Unicode(false)]
        public string? UserName { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Email { get; set; }

        public DateOnly? Dob { get; set; }

        public DateOnly? Doj { get; set; }

        [StringLength(25)]
        [Unicode(false)]
        public string? Password { get; set; }

    }
}
