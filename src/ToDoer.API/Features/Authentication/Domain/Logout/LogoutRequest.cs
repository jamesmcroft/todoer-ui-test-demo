namespace ToDoer.API.Features.Authentication.Domain.Logout
{
    using ToDoer.API.Infrastructure.Identity;

    public class LogoutRequest : AuthenticatedRequestBase
    {
        public LogoutRequest(AuthenticatedUser user)
            : base(user)
        {
        }
    }
}