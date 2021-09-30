namespace ToDoer.UITests.Tests.Tasks
{
    using System;
    using Legerity;
    using NUnit.Framework;
    using ToDoer.UITests.Infrastructure.Tests;
    using ToDoer.UITests.Pages;

    [TestFixtureSource(nameof(TestPlatformOptions))]
    public class WhenCompletingTasks : BaseTestFixture
    {
        public WhenCompletingTasks(AppManagerOptions options)
            : base(options)
        {
        }

        [Test]
        public void ShouldMoveTaskToCompletedTasks()
        {
            // Arrange
            var taskListName = $"Task List {DateTime.UtcNow.Ticks}";
            var taskName = $"Task {DateTime.UtcNow.Ticks}";

            var page = new HomePage()
                .AddNewTaskList(taskListName)
                .AddNewTaskToTaskList(taskListName, taskName);

            // Act
            page.CompleteTaskOnTaskList(taskListName, taskName);

            // Assert
            page.TasksSidebar.VerifyTaskCompleted(taskName);

            Cleanup(page, taskListName);
        }

        private static void Cleanup(HomePage page, string taskListName)
        {
            page.DeleteTaskList(taskListName);
        }
    }
}