using Microsoft.Extensions.Logging;
using SplitExpense.Core.RepositoryContracts;
using SplitExpense.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Infra.Repository
{
    public class ExpenseRepository: IExpenseRepository
    {
        private readonly ILogger _logger;
        private SplitExpenseContext _context;
        public ExpenseRepository(ILogger<ExpenseRepository> logger, SplitExpenseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Guid AddExpense(Expense expense)
        {
            _logger.LogInformation("Addin a new Expense to the records");
            expense.ExpenseId = Guid.NewGuid();
            _context.Add(expense);
            _context.SaveChanges();
            return expense.ExpenseId;
        }

        public IEnumerable<Expense> GetExpenses(Guid userId)
        {
            _logger.LogInformation("Retrieving expenses");
            return _context.Expenses.ToList();
        }
    }
}
