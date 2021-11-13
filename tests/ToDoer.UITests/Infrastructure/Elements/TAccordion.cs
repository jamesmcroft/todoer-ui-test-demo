namespace ToDoer.UITests.Infrastructure.Elements
{
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using Legerity.Web.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TAccordion : WebElementWrapper
    {
        public TAccordion(IWebElement element)
            : base(element)
        {
        }

        public TAccordion(RemoteWebElement element)
            : base(element)
        {
        }

        public static implicit operator TAccordion(RemoteWebElement element)
        {
            return new TAccordion(element);
        }

        public Button HeaderButton => this.Element.FindWebElement(By.ClassName("accordion-header"));

        public RemoteWebElement Body => this.Element.FindWebElement(By.ClassName("accordion-body"));

        public string Header => this.HeaderButton.FindElement(By.ClassName("accordion-button")).Text;

        public bool IsOpen => this.Element.FindElement(By.ClassName("accordion-collapse")).HasClass("show");

        public void Open()
        {
            if (!IsOpen)
            {
                this.HeaderButton.Click();
            }
        }

        public void Close()
        {
            if (IsOpen)
            {
                this.HeaderButton.Click();
            }
        }
    }
}