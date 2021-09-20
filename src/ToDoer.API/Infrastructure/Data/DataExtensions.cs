namespace ToDoer.API.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DataExtensions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, string sqlConnectionString, bool isProduction = false)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                if (!isProduction)
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }

                options.UseSqlServer(sqlConnectionString);
            });

            return services;
        }
    }
}
