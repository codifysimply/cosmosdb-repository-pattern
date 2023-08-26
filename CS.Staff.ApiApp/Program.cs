using CS.Staff.ApiApp.Extensions;
using Microsoft.Azure.Cosmos;
using System.Configuration;

namespace CS.Staff.ApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            var configuration = builder.Configuration;

            CosmosClientOptions cosmosClientOptions = new CosmosClientOptions()
            {
                SerializerOptions = new() { IgnoreNullValues = true },
            };

            builder.Services.AddSingleton(s => new CosmosClient(configuration.GetValue<string>("AccountEndpoint"), configuration.GetValue<string>("AuthKey"), cosmosClientOptions));

            builder.Services.AddDepartmentRepository(settings =>
            {
                settings.DatabaseId = configuration.GetValue<string>("DatabaseId");
                settings.ContainerId = configuration.GetValue<string>("DepartmentContainer"); ;
            });

            builder.Services.AddEmployeeRepository(settings =>
            {
                settings.DatabaseId = configuration.GetValue<string>("DatabaseId");
                settings.ContainerId = configuration.GetValue<string>("EmployeeContainer"); ;
            });

            builder.Services.AddProjectRepository(settings =>
            {
                settings.DatabaseId = configuration.GetValue<string>("DatabaseId");
                settings.ContainerId = configuration.GetValue<string>("ProjectContainer"); ;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}