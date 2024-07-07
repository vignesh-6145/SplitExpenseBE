using SplitExpense.Core.ViewModels;
using SplitExpense.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.ServiceContracts
{
    public interface IUserService
    {
        IEnumerable<User> GetUser();
        User AuthenticateUser(string email, string password);
        bool IsValidUser(string email);
        Guid RegisterUser(UserRegistration user);
    }
}
