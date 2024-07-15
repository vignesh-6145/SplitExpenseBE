using AutoMapper;
using Microsoft.Extensions.Logging;
using SplitExpense.Core.RepositoryContracts;
using SplitExpense.Core.ServiceContracts;
using SplitExpense.Core.ViewModels;
using SplitExpense.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Domain.Services
{
    public class ExpenseService:IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, ILogger<ExpenseService> logger, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public Guid AddExpense(ExpenseInformation expense)
        {
            _logger.LogInformation("Service initiated to Add an expense");
            var Expense = _mapper.Map<Expense>(expense);
            return _expenseRepository.AddExpense(Expense);
        }

        public IEnumerable<Expense> GetUserExpenses(Guid userId)
        {
            _logger.LogInformation("Service initiated to retrieve expenses");
            var Expenses = _expenseRepository.GetExpenses(userId);
            return Expenses;
        }
    }
}
