namespace ToDoer.API.Infrastructure.Exceptions
{
    using System;
    using MADE.Web.Exceptions;
    using MADE.Web.Extensions;
    using Microsoft.Extensions.DependencyInjection;

    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddHttpContextExceptionHandler<Exception, DefaultExceptionHandler>();

            return services;
        }
    }
}
