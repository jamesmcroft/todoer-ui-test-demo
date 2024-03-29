﻿namespace ToDoer.UITests.Tests.Tasks
{
    using System;
    using Legerity;
    using NUnit.Framework;
    using ToDoer.UITests.Infrastructure.Tests;
    using ToDoer.UITests.Pages;

    [TestFixtureSource(nameof(TestPlatformOptions))]
    public class WhenEditingTasks : BaseTestFixture
    {
        public WhenEditingTasks(AppManagerOptions options)
            : base(options)
        {
        }

        [Test]
        public void ShouldUpdateExistingTaskWithNewName()
        {
            // Arrange
            var taskListName = $"Task List {DateTime.UtcNow.Ticks}";
            var taskName = $"Task {DateTime.UtcNow.Ticks}";

            var page = new HomePage()
                .AddNewTaskList(taskListName)
                .AddNewTaskToTaskList(taskListName, taskName);

            var newTaskName = $"Edit Task {DateTime.UtcNow.Ticks}";

            // Act
            page.EditTaskOnTaskList(taskListName, taskName, newTaskName, string.Empty);

            // Assert
            page.TasksSidebar.VerifyTaskShown(newTaskName);

            Cleanup(page, taskListName);
        }

        private static void Cleanup(HomePage page, string taskListName)
        {
            page.DeleteTaskList(taskListName);
        }
    }
}