namespace ToDoer.UITests.Pages.Authentication
{
    using Legerity.Extensions;
    using Legerity.Pages;
    using Legerity.Web.Elements.Core;
    using OpenQA.Selenium;

    public class LoginPage : BasePage
    {
        protected override By Trait => By.XPath(".//h1[contains(text(), 'Login')]");

        public TextInput EmailInput => this.App.FindWebElement(By.Id("emailInput"));

        public TextInput PasswordInput => this.App.FindWebElement(By.Id("passwordInput"));

        public Button LoginButton => this.App.FindWebElement(By.Id("loginButton"));

        public HomePage Login(string email, string password)
        {
            EmailInput.SetText(email);
            PasswordInput.SetText(password);
            LoginButton.Click();
            return new HomePage();
        }
    }
}