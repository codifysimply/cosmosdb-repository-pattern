using CS.Staff.Repositories.Interfaces;
using CS.Staff.Repositories;
using CS.Staff.Models;

namespace CS.Staff.ApiApp.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddDepartmentRepository(this IServiceCollection services , Action<DatabaseSettings> configureSettings)
        {
            return services.AddScoped<IDepartmentRepository>(serviceProvider =>
            {
                var settings = new DatabaseSettings(); 
                configureSettings(settings); 
                return ActivatorUtilities.CreateInstance<DepartmentRepository>(serviceProvider , settings); 
            });
        }

        public static IServiceCollection AddEmployeeRepository(this IServiceCollection services, Action<DatabaseSettings> configureSettings)
        {
            return services.AddScoped<IEmployeeRepository>(serviceProvider =>
            {
                var settings = new DatabaseSettings();
                configureSettings(settings);
                return ActivatorUtilities.CreateInstance<EmployeeRepository>(serviceProvider, settings);
            });
        }

        public static IServiceCollection AddProjectRepository(this IServiceCollection services, Action<DatabaseSettings> configureSettings)
        {
            return services.AddScoped<IProjectRepository>(serviceProvider =>
            {
                var settings = new DatabaseSettings();
                configureSettings(settings);
                return ActivatorUtilities.CreateInstance<ProjectRepository>(serviceProvider, settings);
            });
        }
    }
}
