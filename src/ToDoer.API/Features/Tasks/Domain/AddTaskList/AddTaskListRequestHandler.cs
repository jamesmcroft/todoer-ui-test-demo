namespace ToDoer.API.Features.Tasks.Domain.AddTaskList
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Infrastructure.Data;

    public class AddTaskListRequestHandler : IRequestHandler<AddTaskListRequest>
    {
        private readonly AppDbContext dbContext;

        public AddTaskListRequestHandler(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddTaskListRequest request, CancellationToken cancellationToken)
        {
            var taskList = new TaskList(request);
            await dbContext.AddAsync(taskList, cancellationToken);
            await dbContext.SaveChangesAsync(request.User, cancellationToken);
            return Unit.Value;
        }
    }
}