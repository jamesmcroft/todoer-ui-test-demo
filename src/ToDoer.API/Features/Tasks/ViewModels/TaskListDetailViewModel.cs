namespace ToDoer.API.Features.Tasks.ViewModels
{
    using System.Collections.Generic;

    public class TaskListDetailViewModel : TaskListSummaryViewModel
    {
        public IEnumerable<TaskDetailViewModel> Tasks { get; set; }
    }
}