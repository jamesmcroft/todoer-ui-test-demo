namespace ToDoer.UITests.Infrastructure.Elements
{
    using Legerity.Extensions;
    using Legerity.Web.Elements;
    using Legerity.Web.Elements.Core;
    using Legerity.Web.Extensions;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class TAccordion : WebElementWrapper
    {
        private readonly By headerQuery = By.ClassName("accordion-header");

        private readonly By bodyQuery = By.ClassName("accordion-body");

        private readonly By accordionButtonQuery = By.ClassName("accordion-button");

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

        public Button HeaderButton => this.Element.FindWebElement(this.headerQuery);

        public RemoteWebElement Body => this.Element.FindWebElement(this.bodyQuery);

        public string Header => this.HeaderButton.FindElement(this.accordionButtonQuery).Text;

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