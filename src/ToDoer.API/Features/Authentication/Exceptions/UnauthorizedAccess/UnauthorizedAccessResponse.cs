namespace ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess
{
    using MADE.Web.Exceptions;

    public class UnauthorizedAccessResponse : ExceptionResponse<UnauthorizedAccessException>
    {
        public UnauthorizedAccessResponse(UnauthorizedAccessException exception)
            : base("UnauthorizedAccess", exception.Message, exception)
        {
        }
    }
}