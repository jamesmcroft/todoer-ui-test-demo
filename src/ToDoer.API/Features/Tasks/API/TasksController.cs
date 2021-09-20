namespace ToDoer.API.Features.Tasks.API
{
    using System.Threading;
    using System.Threading.Tasks;
    using Authentication.Domain.Login;
    using Domain.GetMyTaskLists;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using ToDoer.API.Infrastructure.API;
    using ToDoer.API.Infrastructure.Identity;

    [ApiController]
    [Route("api/tasks")]
    [ApiVersion("1.0")]
    public class TasksController : BaseController
    {
        public TasksController(IMediator mediator, IAuthenticatedUserAccessor userAccessor)
            : base(mediator, userAccessor)
        {
        }

        [HttpGet]
        [Route("my")]
        public async Task<IActionResult> GetMyTaskListsAsync(
            CancellationToken cancellationToken = default)
        {
            return await MediatedJsonResultAsync(
                new GetMyTaskListsRequest(UserAccessor.User),
                cancellationToken: cancellationToken);
        }
    }
}