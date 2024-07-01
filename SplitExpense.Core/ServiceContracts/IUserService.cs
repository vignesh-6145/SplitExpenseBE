using SplitExpense.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.ServiceContracts
{
    public interface IUserService
    {
        string GetUser();

        bool IsValidUser(string Email);
        Guid RegisterUser(UserRegistration user);
    }
}
