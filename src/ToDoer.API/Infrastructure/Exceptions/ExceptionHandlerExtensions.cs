namespace ToDoer.API.Infrastructure.Exceptions
{
    using System;
    using MADE.Web.Exceptions;
    using MADE.Web.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess;
    using UnauthorizedAccessException =
        ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess.UnauthorizedAccessException;

    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services
                .AddHttpContextExceptionHandler<UnauthorizedAccessException, UnauthorizedAccessExceptionHandler>()
                .AddHttpContextExceptionHandler<Exception, DefaultExceptionHandler>();

            return services;
        }
    }
}