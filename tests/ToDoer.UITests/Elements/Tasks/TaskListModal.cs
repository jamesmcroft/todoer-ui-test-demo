namespace ToDoer.UITests.Elements.Tasks
{
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TaskListModal : WebElementWrapper
    {
        public TaskListModal(IWebElement element)
            : base(element)
        {
        }

        public TaskListModal(RemoteWebElement element)
            : base(element)
        {
        }

        public static implicit operator TaskListModal(RemoteWebElement element)
        {
            return new TaskListModal(element);
        }

        public TextInput NameInput => Element.FindWebElement(By.Id("taskListNameInput"));

        public Button SaveButton => Element.FindWebElement(By.ClassName("btn-primary"));

        public Button CancelButton => Element.FindWebElement(By.ClassName("btn-secondary"));

        public void SetTaskListName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                NameInput.ClearText();
            }
            else
            {
                NameInput.SetText(name);
            }
        }

        public void Save()
        {
            SaveButton.Click();
        }

        public void Cancel()
        {
            CancelButton.Click();
        }
    }
}