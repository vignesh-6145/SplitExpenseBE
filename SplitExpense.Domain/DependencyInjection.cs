using Microsoft.Extensions.DependencyInjection;
using SplitExpense.Core.ServiceContracts;
using SplitExpense.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IExpenseService, ExpenseService>();
            return services;
        }
    }
}
