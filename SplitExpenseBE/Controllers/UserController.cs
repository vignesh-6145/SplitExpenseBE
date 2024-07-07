using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SplitExpense.Core.Authentication.Basic.Attributes;
using SplitExpense.Core.Exceptions;
using SplitExpense.Core.Models;
using SplitExpense.Core.ServiceContracts;
using SplitExpense.Core.ViewModels;
using System.ComponentModel.DataAnnotations;

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
        [BasicAuthorization]
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
        [HttpPost("Login")]
        public IActionResult LoginUser([FromBody] AuthenticateModel model)
        {
            try
            {
                var isValidUser = _userService.IsValidUser(model.Username);
                if(!isValidUser)
                {
                    throw new UserNotFoundException(model.Username);
                }
                var authenticatedUser = _userService.AuthenticateUser(model.Username,model.Password);
                if(authenticatedUser == null )
                {
                    return Unauthorized("Please check your credentials");
                }
                return Ok(authenticatedUser);


            }
            catch(UnauthorizedAccessException uae)
            {
                return Unauthorized(uae.Message);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
