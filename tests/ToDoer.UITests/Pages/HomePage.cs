namespace ToDoer.UITests.Pages
{
    using Legerity.Extensions;
    using OpenQA.Selenium;
    using ToDoer.UITests.Elements.Tasks;
    using ToDoer.UITests.Infrastructure.Pages;

    public class HomePage : BaseAppPage
    {
        public TaskListSidebar TaskListSidebar => App.FindWebElement(By.Id("taskListSidebar"));

        public TasksSidebar TasksSidebar => App.FindWebElement(By.Id("tasksSidebar"));

        public HomePage AddNewTaskList(string taskListName)
        {
            TaskListSidebar.AddTaskList(taskListName);
            return this;
        }

        public HomePage EditTaskList(string taskListName, string newTaskListName)
        {
            TaskListSidebar.EditTaskList(taskListName, newTaskListName);
            return this;
        }

        public HomePage DeleteTaskList(string taskListName)
        {
            TaskListSidebar.DeleteTaskList(taskListName);
            return this;
        }

        public HomePage SelectTaskList(string taskListName)
        {
            TaskListSidebar.SelectTaskList(taskListName);
            return this;
        }

        public HomePage AddNewTaskToTaskList(string taskListName, string taskName)
        {
            SelectTaskList(taskListName).TasksSidebar.AddTask(taskName);
            return this;
        }

        public HomePage EditTaskOnTaskList(string taskListName, string taskName, string newTaskName, string newTaskNote)
        {
            SelectTaskList(taskListName).TasksSidebar.EditTask(taskName, newTaskName, newTaskNote);
            return this;
        }
    }
}