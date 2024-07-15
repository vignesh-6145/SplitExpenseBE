using SplitExpense.Core.ViewModels;
using SplitExpense.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.ServiceContracts
{
    public interface IExpenseService
    {
        Guid AddExpense(ExpenseInformation expense);
        IEnumerable<Expense> GetUserExpenses(Guid userId);
    }
}
