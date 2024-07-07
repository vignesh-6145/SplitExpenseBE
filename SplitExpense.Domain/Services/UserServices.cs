using AutoMapper;
using SplitExpense.Core.Exceptions;
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
        public IEnumerable<User> GetUser()
        {
            return _userRepository.GetUsers();
        }

        public Guid RegisterUser(UserRegistration user)
        {
            var UserObj = _mapper.Map<User>(user);
            UserObj.IsActive = true;
            return _userRepository.InsertUser(UserObj);
            
        }

        public bool IsValidUser(string Email)
        {
            try
            {
                var userEmails = _userRepository.GetUsers().Select(usr => usr.Email).ToList();
                return userEmails.Contains(Email);
            }catch(Exception ex)
            {
                throw new UserNotFoundException(Email);
            }
        }

        public User AuthenticateUser(string email, string password)
        {
            try
            {
                var user = _userRepository.GetUsers()
                                                .First(usr => usr.Email == email);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }
                if(user.Password!=password)
                {
                    throw new UnauthorizedAccessException("Please check your credentials");
                }
                return user;
            }catch(UserNotFoundException ex)
            {
                throw new UserNotFoundException(email);
            }catch(UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
