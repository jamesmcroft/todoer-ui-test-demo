namespace ToDoer.API.Features.Tasks.Domain.GetTaskList
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Tasks.Exceptions.TaskListNotFound;
    using ToDoer.API.Features.Tasks.Projections;
    using ToDoer.API.Features.Tasks.QueryFilters;
    using ToDoer.API.Features.Tasks.ViewModels;
    using ToDoer.API.Infrastructure.Data;

    public class GetTaskListRequestHandler : IRequestHandler<GetTaskListRequest, TaskListDetailViewModel>
    {
        private readonly AppDbContext dbContext;

        public GetTaskListRequestHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TaskListDetailViewModel> Handle(
            GetTaskListRequest request,
            CancellationToken cancellationToken)
        {
            var taskListViewModel = await this.dbContext
                .GetMyTaskList(request.User.Id, request.TaskListId)
                .Select(TaskListProjections.ToTaskListDetailViewModel())
                .FirstOrDefaultAsync(cancellationToken);

            if (taskListViewModel == null)
            {
                throw new TaskListNotFoundException(request.TaskListId, "The task list could not be found");
            }

            return taskListViewModel;
        }
    }
}