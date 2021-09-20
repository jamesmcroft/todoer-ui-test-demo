namespace ToDoer.API.Features.Tasks.API
{
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
    }
}