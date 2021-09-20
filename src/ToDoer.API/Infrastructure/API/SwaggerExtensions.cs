namespace ToDoer.API.Infrastructure.API
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services, string title = "Service", Action<SwaggerGenOptions> configure = default)
        {
            services.AddSwaggerGen(c =>
            {
                configure?.Invoke(c);

                // Creates the API version docs based on the .NET API version descriptor.
                IApiVersionDescriptionProvider apiVersionProvider = services.BuildServiceProvider()
                                                                            .GetService<IApiVersionDescriptionProvider>();

                if (apiVersionProvider != null)
                {
                    foreach (ApiVersionDescription description in apiVersionProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerDoc(description.GroupName, CreateApiInfoForVersion(title, description));
                    }
                }
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, string title = "Service", IApiVersionDescriptionProvider apiVersionProvider = default)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (apiVersionProvider != null)
                {
                    foreach (ApiVersionDescription description in apiVersionProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{title} {description.ApiVersion}");
                    }
                }

                c.RoutePrefix = string.Empty;
            });

            return app;
        }

        private static OpenApiInfo CreateApiInfoForVersion(string serviceName, ApiVersionDescription version)
        {
            var info = new OpenApiInfo { Title = serviceName, Version = version.ApiVersion.ToString(), };

            if (version.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
