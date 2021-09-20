namespace ToDoer.API.Infrastructure.Identity
{
    using MediatR;

    public abstract class AuthenticatedRequestBase : IRequest
    {
        public AuthenticatedRequestBase(AuthenticatedUser user)
        {
            this.User = user;
        }

        public AuthenticatedUser User { get; protected set; }
    }
}