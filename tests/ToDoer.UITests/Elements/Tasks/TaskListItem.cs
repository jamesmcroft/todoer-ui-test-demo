namespace ToDoer.UITests.Elements.Tasks
{
    using Legerity;
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using Legerity.Web.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TaskListItem : WebElementWrapper
    {
        public TaskListItem(IWebElement element)
            : base(element)
        {
        }

        public TaskListItem(RemoteWebElement element)
            : base(element)
        {
        }

        public static implicit operator TaskListItem(RemoteWebElement element)
        {
            return new TaskListItem(element);
        }

        public string Name => Element.FindElement(By.ClassName("task-item-title")).GetInnerHtml();

        public CheckBox CompleteCheckBox => Element.FindWebElement(By.ClassName("task-item-complete"));

        public Button EditButton => Element
            .FindElement(By.ClassName("task-item-edit"))
            .FindWebElement(By.TagName("span"));

        public Button DeleteButton => Element
            .FindElement(By.ClassName("task-item-delete"))
            .FindWebElement(By.TagName("span"));

        public TaskModal TaskModal => AppManager.App.FindWebElement(By.Id("editTaskModal"));

        public void MarkAsComplete()
        {
            CompleteCheckBox.CheckOn();
        }

        public void MarkAsIncomplete()
        {
            CompleteCheckBox.CheckOff();
        }

        public void Edit(string name, string note)
        {
            EditButton.Click();
            TaskModal.SetTaskName(name);
            TaskModal.SetTaskNote(note);
            TaskModal.Save();
        }
    }
}