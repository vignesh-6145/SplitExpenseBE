using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.Exceptions
{
    public class ExpenseNotFoundException: Exception
    {
        public ExpenseNotFoundException(Guid expenseID):base($"No Expense found with id - {expenseID}") { }
    }
}
