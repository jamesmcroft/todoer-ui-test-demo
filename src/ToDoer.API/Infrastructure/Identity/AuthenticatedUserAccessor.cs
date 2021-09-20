namespace ToDoer.API.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Http;

    public class AuthenticatedUserAccessor : MADE.Web.Identity.AuthenticatedUserAccessor, IAuthenticatedUserAccessor
    {
        public AuthenticatedUserAccessor(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        public AuthenticatedUser User => new(ClaimsPrincipal);
    }
}
