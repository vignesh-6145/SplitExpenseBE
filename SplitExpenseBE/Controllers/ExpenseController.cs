using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SplitExpense.Core.Authentication.Basic.Attributes;
using SplitExpense.Core.ServiceContracts;
using SplitExpense.Core.ViewModels;

namespace SplitExpenseBE.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [BasicAuthorization]
    public class ExpenseController:Controller
    {
        private readonly ILogger _logger;
        private readonly IExpenseService _expenseService;
        public ExpenseController(ILogger<ExpenseController> logger, IExpenseService expenseService) {
            this._logger = logger;
            this._expenseService = expenseService;
        }
        [HttpPost("addExpense")]
        public IActionResult AddExpense([FromBody]ExpenseInformation expense)
        {
            _logger.LogInformation("Received request to create an expense for user-{userID}",expense.UserId);
            try
            {
                return Ok(_expenseService.AddExpense(expense));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getExpenses/{userId}")]
        public IActionResult RetrieveUserExpenses(Guid userId)
        {
            _logger.LogInformation("Received request to retrieve expenses for user-{userID}", userId);
            try
            {
                return Ok(_expenseService.GetUserExpenses(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
