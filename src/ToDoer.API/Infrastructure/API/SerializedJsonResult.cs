namespace ToDoer.API.Infrastructure.API
{
    using System;
    using System.Net;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using MADE.Web.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Newtonsoft.Json;

    public class SerializedJsonResult : ActionResult, IStatusCodeActionResult
    {
        public SerializedJsonResult(object value, HttpStatusCode httpStatusCode = HttpStatusCode.OK, JsonSerializerSettings serializerSettings = default)
        {
            Value = value;
            HttpStatusCode = httpStatusCode;
            SerializerSettings = serializerSettings;
        }

        public object Value { get; }

        public HttpStatusCode HttpStatusCode { get; }

        public JsonSerializerSettings SerializerSettings { get; }

        public int? StatusCode => (int)HttpStatusCode;

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;

            ExceptionDispatchInfo exceptionDispatchInfo = null;

            try
            {
                await response.WriteJsonAsync(HttpStatusCode, Value, SerializerSettings);
            }
            catch (Exception ex)
            {
                exceptionDispatchInfo = ExceptionDispatchInfo.Capture(ex);
            }
            finally
            {
                exceptionDispatchInfo?.Throw();
            }
        }
    }
}