using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplitExpense.Infra;
using SplitExpense.Core.ViewModels;

namespace SplitExpense.Domain.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() {

            CreateMap<User, UserRegistration>();
            CreateMap<UserRegistration, User>();
        }    
    }
}
