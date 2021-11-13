namespace ToDoer.UITests.Infrastructure.Pages
{
    using Legerity.Pages;
    using OpenQA.Selenium;

    public class BaseAppPage : BasePage
    {
        protected override By Trait => By.Id("taskListSidebar");
    }
}