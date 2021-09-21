namespace ToDoer.API.Infrastructure.Exceptions
{
    using System;
    using System.Net;

    public class HttpResponseException : Exception
    {
        public HttpResponseException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}