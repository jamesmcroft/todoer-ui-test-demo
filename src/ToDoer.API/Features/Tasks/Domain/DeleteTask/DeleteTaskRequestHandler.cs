namespace ToDoer.API.Features.Tasks.Domain.DeleteTask
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.EFCore.Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Features.Tasks.Domain.DeleteTask.Exceptions;
    using ToDoer.API.Features.Tasks.Exceptions.TaskListNotFound;
    using ToDoer.API.Features.Tasks.Exceptions.TaskNotFound;
    using ToDoer.API.Features.Tasks.QueryFilters;
    using ToDoer.API.Infrastructure.Data;

    public class DeleteTaskRequestHandler : IRequestHandler<DeleteTaskRequest>
    {
        private readonly AppDbContext dbContext;

        public DeleteTaskRequestHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            var taskList = await GetTaskListAsync(request, cancellationToken);
            var task = GetTask(request, taskList);

            if (await this.dbContext.TryAsync(async context =>
            {
                context.Remove(task);
                await context.SaveChangesAsync(request.User, cancellationToken);
            }))
            {
                return Unit.Value;
            }

            throw new DeleteTaskFailedException($"Failed to remove '{task.Name}'");
        }

        private static TaskListTask GetTask(DeleteTaskRequest request, TaskList taskList)
        {
            var task = taskList.Tasks.FirstOrDefault(x => x.Id == request.TaskId);
            if (task == null)
            {
                throw new TaskNotFoundException(
                    request.TaskListId,
                    request.TaskId,
                    "The task could not be found to delete");
            }

            return task;
        }

        private async Task<TaskList> GetTaskListAsync(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            var taskList = await this.dbContext
                .GetMyTaskList(request.User.Id, request.TaskListId)
                .WithTasks()
                .FirstOrDefaultAsync(cancellationToken);

            if (taskList == null)
            {
                throw new TaskListNotFoundException(
                    request.TaskListId,
                    "A task list could not be found to delete a task from");
            }

            return taskList;
        }
    }
}