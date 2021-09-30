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
    public class WhenAddingTaskLists : BaseTestFixture
    {
        public WhenAddingTaskLists(AppManagerOptions options)
            : base(options)
        {
        }

        [Test]
        public void ShouldAddTaskListWithName()
        {
            // Arrange
            var page = new HomePage();
            var taskListName = $"Tasks {DateTime.UtcNow.Ticks}";

            // Act
            page.AddNewTaskList(taskListName);

            // Assert
            page.VerifyElementShown(ByExtras.Text(taskListName));

            Cleanup(page, taskListName);
        }

        [Test]
        public void ShouldNotAddTaskListIfNameEmpty()
        {
            // Arrange
            var page = new HomePage();

            // Act
            page.AddNewTaskList(string.Empty);

            // Assert
            page.TaskListSidebar.TaskListModal.IsVisible.ShouldBe(true);
            page.TaskListSidebar.TaskListModal.NameInput.Element.HasClass("is-invalid").ShouldBe(true);
        }

        private static void Cleanup(HomePage page, string taskListName)
        {
            page.DeleteTaskList(taskListName);
        }
    }
}