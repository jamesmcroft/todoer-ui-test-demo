namespace ToDoer.UITests.Elements.Tasks
{
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TaskModal : WebElementWrapper
    {
        public TaskModal(IWebElement element)
            : base(element)
        {
        }

        public TaskModal(RemoteWebElement element)
            : base(element)
        {
        }

        public static implicit operator TaskModal(RemoteWebElement element)
        {
            return new TaskModal(element);
        }

        public TextInput NameInput => Element.FindWebElement(By.Id("taskNameInput"));

        public TextArea NoteInput => Element.FindWebElement(By.Id("taskNoteInput"));

        public Button SaveButton => Element.FindWebElement(By.ClassName("btn-primary"));

        public Button CancelButton => Element.FindWebElement(By.ClassName("btn-secondary"));

        public void SetTaskName(string name)
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

        public void SetTaskNote(string note)
        {
            if (string.IsNullOrWhiteSpace(note))
            {
                NoteInput.ClearText();
            }
            else
            {
                NoteInput.SetText(note);
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