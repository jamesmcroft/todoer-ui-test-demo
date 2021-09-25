namespace ToDoer.UITests.Elements.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using Legerity;
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TaskListSidebar : WebElementWrapper
    {
        public TaskListSidebar(IWebElement element)
            : base(element)
        {
        }

        public TaskListSidebar(RemoteWebElement element)
            : base(element)
        {
        }

        public static implicit operator TaskListSidebar(RemoteWebElement element)
        {
            return new TaskListSidebar(element);
        }

        public Button AddTaskListButton => Element.FindWebElement(By.Id("addTaskListButton"));

        public IEnumerable<TaskListSummary> TaskLists =>
            Element.FindWebElements(By.ClassName("task-list-item")).Select(x => new TaskListSummary(x));

        public TaskListModal TaskListModal => AppManager.App.FindWebElement(By.Id("addUpdateTaskListModal"));

        public void AddTaskList(string taskListName)
        {
            AddTaskListButton.Click();
            TaskListModal.SetTaskListName(taskListName);
            TaskListModal.Save();
        }

        public void EditTaskList(string taskListName, string newTaskListName)
        {
            VerifyTaskListShown(taskListName);
            var taskList = TaskLists.FirstOrDefault(x => x.Name == taskListName);
            taskList.Edit(newTaskListName);
        }

        public void DeleteTaskList(string taskListName)
        {
            VerifyTaskListShown(taskListName);
            var taskList = TaskLists.FirstOrDefault(x => x.Name == taskListName);
            taskList.Delete();
        }

        public void SelectTaskList(string taskListName)
        {
            VerifyTaskListShown(taskListName);
            var taskList = TaskLists.FirstOrDefault(x => x.Name == taskListName);
            taskList.Select();
        }

        public void VerifyTaskListShown(string taskListName)
        {
            VerifyElementShown(ByExtras.Text(taskListName));
        }
    }
}