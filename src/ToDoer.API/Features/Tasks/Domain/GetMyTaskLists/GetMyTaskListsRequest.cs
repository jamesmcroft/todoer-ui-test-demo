namespace ToDoer.API.Features.Tasks.Domain.GetMyTaskLists
{
    using System;
    using System.Collections.Generic;
    using ToDoer.API.Features.Tasks.ViewModels;
    using ToDoer.API.Infrastructure.Identity;

    public class GetMyTaskListsRequest : AuthenticatedRequestBase<IEnumerable<TaskListSummaryViewModel>>
    {
        public GetMyTaskListsRequest(AuthenticatedUser user)
            : base(user)
        {
            UserId = user.Id;
        }

        public Guid UserId { get; set; }
    }
}