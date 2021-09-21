namespace ToDoer.API.Features.Tasks.Domain.DeleteTaskList
{
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.EFCore.Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Features.Tasks.Domain.DeleteTaskList.Exceptions;
    using ToDoer.API.Features.Tasks.Exceptions.TaskListNotFound;
    using ToDoer.API.Features.Tasks.QueryFilters;
    using ToDoer.API.Infrastructure.Data;

    public class DeleteTaskListRequestHandler : IRequestHandler<DeleteTaskListRequest>
    {
        private readonly AppDbContext dbContext;

        public DeleteTaskListRequestHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteTaskListRequest listRequest, CancellationToken cancellationToken)
        {
            var taskList = await GetTaskListAsync(listRequest, cancellationToken);

            if (await this.dbContext.TryAsync(async context =>
            {
                context.Remove(taskList);
                await context.SaveChangesAsync(listRequest.User, cancellationToken);
            }))
            {
                return Unit.Value;
            }

            throw new DeleteTaskListFailedException($"Failed to remove '{taskList.Name}'");
        }

        private async Task<TaskList> GetTaskListAsync(
            DeleteTaskListRequest request,
            CancellationToken cancellationToken)
        {
            var taskList = await this.dbContext
                .GetMyTaskList(request.User.Id, request.TaskListId)
                .FirstOrDefaultAsync(cancellationToken);

            if (taskList == null)
            {
                throw new TaskListNotFoundException(
                    request.TaskListId,
                    "The task list could not be found to delete");
            }

            return taskList;
        }
    }
}