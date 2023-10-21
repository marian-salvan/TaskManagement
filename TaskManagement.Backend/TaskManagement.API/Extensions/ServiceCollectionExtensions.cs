using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManagement.API.Services;
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

        //business logic services
        public static void AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUsersService, UsersService>();
            services.TryAddScoped<ITasksService, TasksService>();
        }
    }
}
