using Microsoft.EntityFrameworkCore;
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
            return _context.Expenses
                .Where(expense => expense.UserId==userId)
                .ToList();
        }

        public Expense GetExpense(Guid expenseId)
        {
            _logger.LogInformation("Retrieving expense - {expenseId}",expenseId);
            return _context.Expenses.Find(expenseId);
        }

        public int DeleteExpense(Guid expenseId)
        {
            _logger.LogInformation("Deleting expense with id - {expenseID}",expenseId);
            var expense = _context.Expenses.Find(expenseId);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
                return 1;
            }
            _logger.LogInformation("No expense found with given ID - {expenseId}",expenseId);
            return 0;

        }
    }
}
