using AutoMapper;
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
    public class UserServices: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserServices(IUserRepository userRepository,IMapper mapper) {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public string GetUser()
        {
            Console.WriteLine(_userRepository.GetUsers()==null);
            return ""+_userRepository.GetUsers();
        }

        public Guid RegisterUser(UserRegistration user)
        {
            var UserObj = _mapper.Map<User>(user);
            UserObj.IsActive = true;
            return _userRepository.InsertUser(UserObj);
            
        }

        public bool IsValidUser(string Email)
        {
            var users = _userRepository.GetUsers();
            
            return users.Any(usr => usr.Email==Email);
        }
    }
}
