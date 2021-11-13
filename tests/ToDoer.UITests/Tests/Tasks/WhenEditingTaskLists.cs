namespace ToDoer.UITests.Tests.Tasks
{
    using System;
    using System.Linq;
    using Legerity;
    using Legerity.Extensions;
    using NUnit.Framework;
    using ToDoer.UITests.Infrastructure.Tests;
    using ToDoer.UITests.Pages;

    [TestFixtureSource(nameof(TestPlatformOptions))]
    public class WhenEditingTaskLists : BaseTestFixture
    {
        public WhenEditingTaskLists(AppManagerOptions options)
            : base(options)
        {
        }

        [Test]
        public void ShouldUpdateExistingTaskListWithNewName()
        {
            // Arrange
            var taskListName = $"Tasks {DateTime.UtcNow.Ticks}";
            var page = new HomePage().AddNewTaskList(taskListName)
                .WaitUntil(homePage => homePage.TaskListSidebar.TaskLists.FirstOrDefault(x => x.Name == taskListName) != null, ImplicitWait);

            var newTaskListName = $"Edit Tasks {DateTime.UtcNow.Ticks}";

            // Act
            page.EditTaskList(taskListName, newTaskListName);

            // Assert
            page.VerifyElementShown(ByExtras.Text(newTaskListName));

            Cleanup(page, newTaskListName);
        }

        private static void Cleanup(HomePage page, string taskListName)
        {
            page.DeleteTaskList(taskListName);
        }
    }
}