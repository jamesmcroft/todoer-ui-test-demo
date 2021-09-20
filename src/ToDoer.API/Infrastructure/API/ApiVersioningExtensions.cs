namespace ToDoer.API.Infrastructure.API
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddApiVersionHeaderSupport(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("ApiVersion");
            });

            return services;
        }
    }
}
