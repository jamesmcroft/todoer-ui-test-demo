namespace ToDoer.UITests.Tests.Tasks
{
    using System;
    using Legerity;
    using Legerity.Web.Extensions;
    using NUnit.Framework;
    using Shouldly;
    using ToDoer.UITests.Infrastructure.Tests;
    using ToDoer.UITests.Pages;

    [TestFixtureSource(nameof(TestPlatformOptions))]
    public class WhenAddingTasks : BaseTestFixture
    {
        public WhenAddingTasks(AppManagerOptions options)
            : base(options)
        {
        }

        [Test]
        public void ShouldAddTask()
        {
            // Arrange
            var taskListName = $"Tasks {DateTime.Now:g}";
            var page = new HomePage()
                .AddNewTaskList(taskListName);

            var taskName = $"Task {DateTime.Now:g}";

            // Act
            page.AddNewTaskToTaskList(taskListName, taskName);

            // Assert
            page.TasksSidebar.VerifyTaskShown(taskListName);

            Cleanup(page, taskListName);
        }

        [Test]
        public void ShouldNotAddTaskListIfNameEmpty()
        {
            // Arrange
            var taskListName = $"Tasks {DateTime.Now:g}";
            var page = new HomePage()
                .AddNewTaskList(taskListName);

            // Act
            page.AddNewTaskToTaskList(taskListName, string.Empty);

            // Assert
            page.TasksSidebar.AddTaskInput.Element.HasClass("is-invalid").ShouldBeTrue();

            Cleanup(page, taskListName);
        }

        private static void Cleanup(HomePage page, string taskListName)
        {
            page.DeleteTaskList(taskListName);
        }
    }
}