using Microsoft.AspNetCore.Mvc;
using SplitExpense.Core.ServiceContracts;
using SplitExpense.Core.ViewModels;

namespace SplitExpenseBE.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Users")]
        public IActionResult GetUsers() {
            
            return Ok(_userService.GetUser());
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserRegistration user)
        {
            //TODO : Add Logging at necessary states
            return Ok(_userService.RegisterUser(user));
        }
    }
}
