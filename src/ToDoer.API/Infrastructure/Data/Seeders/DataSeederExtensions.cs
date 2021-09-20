namespace ToDoer.API.Infrastructure.Data.Seeders
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DataSeederExtensions
    {
        public static IServiceCollection AddDataSeeder<T>(this IServiceCollection services)
            where T : class, IDataSeeder
        {
            services.AddTransient<IDataSeeder, T>();
            return services;
        }
    }
}
