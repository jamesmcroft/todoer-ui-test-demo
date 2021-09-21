namespace ToDoer.API.Features.Tasks.Domain.AddTaskList
{
    using ToDoer.API.Infrastructure.Identity;

    public class AddTaskListRequest : AuthenticatedRequestBase
    {
        public AddTaskListRequest(AuthenticatedUser user, AddTaskListRequestDto request)
            : base(user)
        {
            Name = request.Name;
        }

        public string Name { get; set; }
    }
}