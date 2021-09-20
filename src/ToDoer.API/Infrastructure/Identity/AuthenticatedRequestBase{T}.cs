namespace ToDoer.API.Infrastructure.Identity
{
    using MediatR;

    public class AuthenticatedRequestBase<T> : IRequest<T>
    {
        public AuthenticatedRequestBase(AuthenticatedUser user)
        {
            this.User = user;
        }

        public AuthenticatedUser User { get; protected set; }
    }
}