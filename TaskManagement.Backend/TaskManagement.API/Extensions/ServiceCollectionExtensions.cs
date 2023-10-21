using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OData.ModelBuilder;
using TaskManagement.API.Integrations;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<IUsersRepository, UsersRepository>();
            services.TryAddScoped<ITasksRepository, TasksRepository>();
        }

        public static void AddExternalServices(this IServiceCollection services)
        {
            services.TryAddScoped<ITaskSummaryGenerator, TaskSummaryGenerator>();
        }

        public static void AddOdataConfiguration(this IServiceCollection services)
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<UserEntity>("Users");
            modelBuilder.EntitySet<TaskEntity>("Tasks");

            services.AddControllers().AddOData(
                options => options.EnableQueryFeatures(null).AddRouteComponents(
                    routePrefix: "odata",
                    model: modelBuilder.GetEdmModel()));
        }
    }
}
