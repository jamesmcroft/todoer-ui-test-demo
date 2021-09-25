namespace ToDoer.UITests.Elements.Tasks
{
    using Legerity;
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using Legerity.Web.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TaskListSummary : WebElementWrapper
    {
        public TaskListSummary(IWebElement element)
            : base(element)
        {
        }

        public TaskListSummary(RemoteWebElement element)
            : base(element)
        {
        }

        public static implicit operator TaskListSummary(RemoteWebElement element)
        {
            return new TaskListSummary(element);
        }

        public string Name => Element.FindElement(By.ClassName("task-list-item-title")).GetInnerHtml();

        public Button EditButton => Element
            .FindElement(By.ClassName("task-list-item-edit"))
            .FindWebElement(By.TagName("span"));

        public Button DeleteButton => Element
            .FindElement(By.ClassName("task-list-item-delete"))
            .FindWebElement(By.TagName("span"));

        public TaskListModal TaskListModal => AppManager.App.FindWebElement(By.Id("addUpdateTaskListModal"));

        public void Edit(string name)
        {
            EditButton.Click();
            TaskListModal.SetTaskListName(name);
            TaskListModal.Save();
        }

        public void Delete()
        {
            DeleteButton.Click();
        }

        public void Select()
        {
            Element.Click();
        }
    }
}