namespace ToDoer.API.Features.Tasks.API
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using ToDoer.API.Features.Tasks.Domain.AddTask;
    using ToDoer.API.Features.Tasks.Domain.AddTaskList;
    using ToDoer.API.Features.Tasks.Domain.DeleteTask;
    using ToDoer.API.Features.Tasks.Domain.DeleteTaskList;
    using ToDoer.API.Features.Tasks.Domain.GetMyTaskLists;
    using ToDoer.API.Features.Tasks.Domain.GetTaskList;
    using ToDoer.API.Features.Tasks.Domain.UpdateTask;
    using ToDoer.API.Features.Tasks.Domain.UpdateTaskList;
    using ToDoer.API.Features.Tasks.ViewModels;
    using ToDoer.API.Infrastructure.API;
    using ToDoer.API.Infrastructure.Identity;

    [ApiController]
    [Route("api/tasks")]
    [ApiVersion("1.0")]
    public class TasksController : BaseController
    {
        public TasksController(IMediator mediator, IAuthenticatedUserAccessor userAccessor)
            : base(mediator, userAccessor)
        {
        }

        [HttpGet]
        [Route("my")]
        [ProducesResponseType(typeof(IEnumerable<TaskListSummaryViewModel>), 200)]
        public async Task<IActionResult> GetMyTaskListsAsync(
            CancellationToken cancellationToken = default)
        {
            return await MediatedJsonResultAsync(
                new GetMyTaskListsRequest(UserAccessor.User),
                cancellationToken: cancellationToken);
        }

        [HttpGet]
        [Route("list/{taskListId:guid}")]
        [ProducesResponseType(typeof(TaskListDetailViewModel), 200)]
        public async Task<IActionResult> GetMyTaskListAsync(
            [FromRoute] Guid taskListId,
            CancellationToken cancellationToken = default)
        {
            return await MediatedJsonResultAsync(
                new GetTaskListRequest(UserAccessor.User, taskListId),
                cancellationToken: cancellationToken);
        }

        [HttpPost]
        [Route("list")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddTaskListAsync([FromBody] AddTaskListRequestDto addTaskList)
        {
            return await MediatedOkAsync(new AddTaskListRequest(UserAccessor.User, addTaskList));
        }

        [HttpPut]
        [Route("list/{taskListId:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTaskListAsync(
            [FromRoute] Guid taskListId,
            [FromBody] UpdateTaskListRequestDto updateTaskList)
        {
            return await MediatedOkAsync(new UpdateTaskListRequest(UserAccessor.User, taskListId, updateTaskList));
        }

        [HttpDelete]
        [Route("list/{taskListId:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteTaskListAsync([FromRoute] Guid taskListId)
        {
            return await MediatedOkAsync(new DeleteTaskListRequest(UserAccessor.User, taskListId));
        }

        [HttpPost]
        [Route("list/{taskListId:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddTaskToTaskListAsync(
            [FromRoute] Guid taskListId,
            [FromBody] AddTaskRequestDto addTask)
        {
            return await MediatedOkAsync(new AddTaskRequest(UserAccessor.User, taskListId, addTask));
        }

        [HttpPut]
        [Route("list/{taskListId:guid}/{taskId:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTaskOnTaskListAsync(
            [FromRoute] Guid taskListId,
            [FromRoute] Guid taskId,
            [FromBody] UpdateTaskRequestDto updateTask)
        {
            return await MediatedOkAsync(new UpdateTaskRequest(UserAccessor.User, taskListId, taskId, updateTask));
        }

        [HttpDelete]
        [Route("list/{taskListId:guid}/{taskId:guid}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteTaskFromTaskListAsync(
            [FromRoute] Guid taskListId,
            [FromRoute] Guid taskId)
        {
            return await MediatedOkAsync(new DeleteTaskRequest(UserAccessor.User, taskListId, taskId));
        }
    }
}