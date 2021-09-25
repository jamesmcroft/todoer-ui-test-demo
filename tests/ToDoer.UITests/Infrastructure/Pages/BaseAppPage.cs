namespace ToDoer.UITests.Infrastructure.Pages
{
    using System;
    using System.Threading;
    using Legerity.Pages;
    using OpenQA.Selenium;

    public class BaseAppPage : BasePage
    {
        private readonly By taskListSidebarQuery = By.Id("taskListSidebar");

        protected override By Trait => this.taskListSidebarQuery;

        public TPage Wait<TPage>(int milliseconds)
            where TPage : BaseAppPage
        {
            return this.Wait<TPage>(TimeSpan.FromMilliseconds(milliseconds));
        }

        public TPage Wait<TPage>(TimeSpan waitTime)
            where TPage : BaseAppPage
        {
            Thread.Sleep(waitTime);
            return this as TPage;
        }
    }
}