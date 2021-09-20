namespace ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess
{
    using System;

    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message)
            : base(message)
        {
        }
    }
}