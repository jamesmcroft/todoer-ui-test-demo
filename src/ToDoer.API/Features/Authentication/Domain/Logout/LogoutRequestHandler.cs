namespace ToDoer.API.Features.Authentication.Domain.Logout
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;

    public class LogoutRequestHandler : IRequestHandler<LogoutRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;

        public LogoutRequestHandler(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public async Task<Unit> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            await this.contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Unit.Value;
        }
    }
}