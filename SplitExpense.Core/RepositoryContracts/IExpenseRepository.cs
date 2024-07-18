using SplitExpense.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.RepositoryContracts
{
    public interface IExpenseRepository
    {
        Guid AddExpense(Expense expense);
        IEnumerable<Expense> GetExpenses(Guid userId);

        Expense GetExpense(Guid expenseId);

        int DeleteExpense(Guid expenseId);
    }
}
