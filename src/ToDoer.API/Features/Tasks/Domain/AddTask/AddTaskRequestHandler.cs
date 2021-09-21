namespace ToDoer.API.Features.Tasks.Domain.AddTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.EFCore.Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Features.Tasks.Domain.AddTask.Exceptions;
    using ToDoer.API.Features.Tasks.Exceptions.TaskListNotFound;
    using ToDoer.API.Features.Tasks.QueryFilters;
    using ToDoer.API.Infrastructure.Data;

    public class AddTaskRequestHandler : IRequestHandler<AddTaskRequest>
    {
        private readonly AppDbContext dbContext;

        public AddTaskRequestHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddTaskRequest request, CancellationToken cancellationToken)
        {
            var taskList = await this.GetTaskListAsync(request, cancellationToken);

            taskList.AddTask(request.Name);

            if (await this.dbContext.TryAsync(async context =>
            {
                context.Update(taskList);
                await context.SaveChangesAsync(request.User, cancellationToken);
            }))
            {
                return Unit.Value;
            }

            throw new AddTaskFailedException($"Failed to add '{request.Name}'");
        }

        private async Task<TaskList> GetTaskListAsync(AddTaskRequest request, CancellationToken cancellationToken)
        {
            var taskList = await this.dbContext
                .GetMyTaskList(request.User.Id, request.TaskListId)
                .WithTasks()
                .FirstOrDefaultAsync(cancellationToken);

            if (taskList == null)
            {
                throw new TaskListNotFoundException(
                    request.TaskListId,
                    "A task list could not be found to add a task to");
            }

            return taskList;
        }
    }
}