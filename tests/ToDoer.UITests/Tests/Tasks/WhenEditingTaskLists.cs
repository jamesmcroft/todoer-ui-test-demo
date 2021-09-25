namespace ToDoer.UITests.Tests.Tasks
{
    using System;
    using Legerity;
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
            var taskListName = $"Tasks {DateTime.Now:g}";
            var page = new HomePage().AddNewTaskList(taskListName).Wait<HomePage>(100);

            var newTaskListName = $"Edit Tasks {DateTime.Now:g}";

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