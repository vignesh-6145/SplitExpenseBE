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
    public class UserRepository : IUserRepository
    {
        private readonly SplitExpenseContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(SplitExpenseContext context,ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

            public IEnumerable<User> GetUsers(){
            return _context.Users;
        }

        public Guid InsertUser(User user)
        {
            _logger.LogInformation("Inserting new user with email : {Email}",user.Email);
            user.UserId = Guid.NewGuid();
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;

        }
    }
}
