namespace ToDoer.API.Infrastructure.Identity
{
    public interface IAuthenticatedUserAccessor : MADE.Web.Identity.IAuthenticatedUserAccessor
    {
        AuthenticatedUser User { get; }
    }
}
