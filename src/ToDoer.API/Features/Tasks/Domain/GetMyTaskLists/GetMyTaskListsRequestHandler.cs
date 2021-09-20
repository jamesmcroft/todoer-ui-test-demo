namespace ToDoer.API.Features.Tasks.Domain.GetMyTaskLists
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Tasks.Projections;
    using ToDoer.API.Features.Tasks.ViewModels;
    using ToDoer.API.Infrastructure.Data;

    public class
        GetMyTaskListsRequestHandler : IRequestHandler<GetMyTaskListsRequest, IEnumerable<TaskListSummaryViewModel>>
    {
        private readonly AppDbContext dbContext;

        public GetMyTaskListsRequestHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskListSummaryViewModel>> Handle(
            GetMyTaskListsRequest request,
            CancellationToken cancellationToken)
        {
            return await this.dbContext.TaskLists
                .AsQueryable()
                .Where(x => x.AssignedToId == request.UserId)
                .OrderBy(x => x.Name)
                .Select(TaskListProjections.ToTaskListSummaryViewModel())
                .ToListAsync(cancellationToken);
        }
    }
}