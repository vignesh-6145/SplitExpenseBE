using SplitExpense.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Core.RepositoryContracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        Guid InsertUser(User user);
    }
}
