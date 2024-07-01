
using Serilog;
using SplitExpense.Domain;
using SplitExpense.Infra;

namespace SplitExpenseBE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDomainServices();
            builder.Services.AddInfraServices(builder.Configuration);
            // Add services to the container.
            builder.Host.UseSerilog();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}
