namespace ToDoer.API.Features.Authentication.API
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ToDoer.API.Features.Authentication.Domain.Login;
    using ToDoer.API.Features.Authentication.Domain.Logout;
    using ToDoer.API.Infrastructure.API;
    using ToDoer.API.Infrastructure.Identity;

    [ApiController]
    [Route("api/auth")]
    [ApiVersion("1.0")]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator mediator, IAuthenticatedUserAccessor userAccessor)
            : base(mediator, userAccessor)
        {
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginRequestDto request,
            CancellationToken cancellationToken = default)
        {
            return await MediatedOkAsync(
                new LoginRequest { Email = request.Email, Password = request.Password },
                cancellationToken);
        }

        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> LogoutAsync(
            CancellationToken cancellationToken = default)
        {
            return await MediatedOkAsync(
                new LogoutRequest(UserAccessor.User),
                cancellationToken);
        }
    }
}