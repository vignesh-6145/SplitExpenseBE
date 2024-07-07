using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string identifier):base($"{identifier} not found in our records") { }
    }
}
