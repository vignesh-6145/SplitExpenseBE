using AutoMapper;
using SplitExpense.Core.ViewModels;
using SplitExpense.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Domain.Profiles
{
    public class ExpenseProfile:Profile
    {
        public ExpenseProfile() {
            CreateMap<Expense, ExpenseInformation>();
            CreateMap<ExpenseInformation, Expense>();
        }
    }
}
