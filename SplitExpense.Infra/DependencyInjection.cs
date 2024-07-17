using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SplitExpense.Core.RepositoryContracts;
using SplitExpense.Infra.Data;
using SplitExpense.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitExpense.Infra
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddDbContext<SplitExpenseContext>(
            options => options.UseSqlServer(
                    configuration.GetConnectionString("localDb")
                    )
                );
            return services;
        }
    }
}
