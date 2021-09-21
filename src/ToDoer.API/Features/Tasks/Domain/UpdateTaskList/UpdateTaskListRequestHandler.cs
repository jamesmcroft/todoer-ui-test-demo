namespace ToDoer.API.Features.Tasks.Domain.UpdateTaskList
{
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.EFCore.Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Features.Tasks.Domain.UpdateTaskList.Exceptions;
    using ToDoer.API.Features.Tasks.Exceptions.TaskListNotFound;
    using ToDoer.API.Features.Tasks.QueryFilters;
    using ToDoer.API.Infrastructure.Data;

    public class UpdateTaskListRequestHandler : IRequestHandler<UpdateTaskListRequest>
    {
        private readonly AppDbContext dbContext;

        public UpdateTaskListRequestHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateTaskListRequest listRequest, CancellationToken cancellationToken)
        {
            var taskList = await GetTaskListAsync(listRequest, cancellationToken);

            if (!taskList.Update(listRequest))
            {
                return Unit.Value;
            }

            if (await this.dbContext.TryAsync(async context =>
            {
                context.Update(taskList);
                await context.SaveChangesAsync(listRequest.User, cancellationToken);
            }))
            {
                return Unit.Value;
            }

            throw new UpdateTaskListFailedException($"Failed to update '{taskList.Name}'");
        }

        private async Task<TaskList> GetTaskListAsync(UpdateTaskListRequest listRequest, CancellationToken cancellationToken)
        {
            var taskList = await this.dbContext
                .GetMyTaskList(listRequest.User.Id, listRequest.TaskListId)
                .FirstOrDefaultAsync(cancellationToken);

            if (taskList == null)
            {
                throw new TaskListNotFoundException(
                    listRequest.TaskListId,
                    "The task list could not be found to update");
            }

            return taskList;
        }
    }
}