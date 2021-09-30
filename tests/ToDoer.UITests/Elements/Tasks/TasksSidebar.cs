namespace ToDoer.UITests.Elements.Tasks
{
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.Elements;
    using Legerity;
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using Legerity.Web.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TasksSidebar : WebElementWrapper
    {
        public TasksSidebar(IWebElement element)
            : base(element)
        {
        }

        public TasksSidebar(RemoteWebElement element)
            : base(element)
        {
        }

        public static implicit operator TasksSidebar(RemoteWebElement element)
        {
            return new TasksSidebar(element);
        }

        public string ListName => Element.FindElement(By.ClassName("tasks-title")).GetInnerHtml();

        public TextInput AddTaskInput => Element.FindWebElement(By.Id("addTaskInput"));

        public IEnumerable<TaskListItem> Tasks =>
            Element.FindElement(By.ClassName("task-group"))
                .FindWebElements(By.ClassName("task-item"))
                .Select(x => new TaskListItem(x));

        public TAccordion CompletedTasksAccordion => Element.FindWebElement(By.Id("completedTasksList"));

        public IEnumerable<TaskListItem> CompletedTasks => CompletedTasksAccordion.Body
            .FindWebElements(By.ClassName("task-item"))
            .Select(x => new TaskListItem(x));

        public void AddTask(string taskName)
        {
            AddTaskInput.SetText(taskName);
            AddTaskInput.Element.SendKeys(Keys.Enter);
        }

        public void VerifyTaskShown(string taskName)
        {
            this.VerifyElementShown(ByExtras.Text(taskName));
        }

        public void EditTask(string taskName, string newTaskName, string newTaskNote)
        {
            VerifyTaskShown(taskName);
            var task = Tasks.FirstOrDefault(x => x.Name == taskName);
            task.Edit(newTaskName, newTaskNote);
        }

        public void CompleteTask(string taskName)
        {
            VerifyTaskShown(taskName);
            var task = Tasks.FirstOrDefault(x => x.Name == taskName);
            task.MarkAsComplete();
        }

        public void VerifyTaskCompleted(string taskName)
        {
            CompletedTasksAccordion.Open();

            var completedTask = CompletedTasks.FirstOrDefault(x => x.Name == taskName);

            if (completedTask == null)
            {
                throw new InvalidElementStateException($"Task list item {taskName} is not in a completed state");
            }
        }
    }
}