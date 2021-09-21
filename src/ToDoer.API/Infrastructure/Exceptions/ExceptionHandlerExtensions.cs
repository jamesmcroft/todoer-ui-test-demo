namespace ToDoer.API.Infrastructure.Exceptions
{
    using System;
    using MADE.Web.Exceptions;
    using MADE.Web.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using ToDoer.API.Features.Tasks.Domain.AddTask.Exceptions;
    using ToDoer.API.Features.Tasks.Domain.DeleteTask.Exceptions;
    using ToDoer.API.Features.Tasks.Domain.DeleteTaskList.Exceptions;
    using ToDoer.API.Features.Tasks.Domain.UpdateTask.Exceptions;
    using ToDoer.API.Features.Tasks.Domain.UpdateTaskList.Exceptions;
    using ToDoer.API.Features.Tasks.Exceptions.TaskListNotFound;
    using ToDoer.API.Features.Tasks.Exceptions.TaskNotFound;
    using UnauthorizedAccessException =
        ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess.UnauthorizedAccessException;

    public static class ExceptionHandlerExtensions
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services
                .AddGenericHttpContextExceptionHandler<UnauthorizedAccessException>()
                .AddGenericHttpContextExceptionHandler<TaskListNotFoundException>()
                .AddGenericHttpContextExceptionHandler<TaskNotFoundException>()
                .AddGenericHttpContextExceptionHandler<AddTaskFailedException>()
                .AddGenericHttpContextExceptionHandler<DeleteTaskFailedException>()
                .AddGenericHttpContextExceptionHandler<DeleteTaskListFailedException>()
                .AddGenericHttpContextExceptionHandler<UpdateTaskFailedException>()
                .AddGenericHttpContextExceptionHandler<UpdateTaskListFailedException>()
                .AddHttpContextExceptionHandler<Exception, DefaultExceptionHandler>();

            return services;
        }

        private static IServiceCollection AddGenericHttpContextExceptionHandler<TException>(
            this IServiceCollection services)
            where TException : HttpResponseException
        {
            services.AddHttpContextExceptionHandler<TException, GenericHttpContextExceptionHandler<TException>>();

            return services;
        }
    }
}