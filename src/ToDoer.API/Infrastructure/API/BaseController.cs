namespace ToDoer.API.Infrastructure.API
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using ToDoer.API.Infrastructure.Data.Serialization;
    using ToDoer.API.Infrastructure.Identity;

    public abstract class BaseController : ControllerBase
    {
        protected BaseController(IMediator mediator, IAuthenticatedUserAccessor userAccessor)
        {
            Mediator = mediator;
            UserAccessor = userAccessor;
        }

        public IMediator Mediator { get; }

        public IAuthenticatedUserAccessor UserAccessor { get; }

        protected virtual IActionResult SerializedJson(
            object value,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            JsonSerializerSettings serializerSettings = null)
        {
            return new SerializedJsonResult(value, statusCode, serializerSettings ?? JsonConstants.SerializerSettings);
        }

        protected virtual async Task<IActionResult> MediatedJsonResultAsync<TResponse>(
            IRequest<TResponse> request,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            JsonSerializerSettings serializerSettings = null,
            CancellationToken cancellationToken = default)
        {
            TResponse result = await Mediator.Send(request, cancellationToken);
            return SerializedJson(result, statusCode, serializerSettings);
        }

        protected virtual async Task<IActionResult> MediatedOkAsync(
            IRequest request,
            CancellationToken cancellationToken = default)
        {
            _ = await Mediator.Send(request, cancellationToken);
            return Ok();
        }
    }
}