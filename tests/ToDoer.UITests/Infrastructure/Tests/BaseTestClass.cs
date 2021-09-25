namespace ToDoer.UITests.Infrastructure.Tests
{
    using System;
    using System.Collections.Generic;
    using Legerity;
    using Legerity.Web;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using OpenQA.Selenium.Remote;
    using ToDoer.UITests.Infrastructure.Configuration;
    using ToDoer.UITests.Pages;
    using ToDoer.UITests.Pages.Authentication;

    public class BaseTestClass
    {
        protected BaseTestClass(AppManagerOptions options)
        {
            Options = options;
        }

        public static IEnumerable<AppManagerOptions> TestPlatformOptions => new List<AppManagerOptions>
        {
            new WebAppManagerOptions(WebAppDriverType.Chrome, Environment.CurrentDirectory)
            {
                Url = "https://localhost:3000/", Maximize = true, ImplicitWait = TimeSpan.FromSeconds(5)
            },
            new WebAppManagerOptions(WebAppDriverType.EdgeChromium, Environment.CurrentDirectory)
            {
                Url = "https://localhost:3000/", Maximize = true, ImplicitWait = TimeSpan.FromSeconds(5)
            }
        };

        protected static RemoteWebDriver App => AppManager.App;

        public UITestSettings Settings { get; set; }

        protected AppManagerOptions Options { get; }

        [SetUp]
        public void StartApp()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.Test.json", true, true)
                .Build();

            Settings = UITestSettings.Build(configuration);

            AppManager.StartApp(Options);

            this.Login();
        }

        [TearDown]
        public void StopApp()
        {
            AppManager.StopApp();
        }

        public HomePage Login()
        {
            return new LoginPage().Login(this.Settings.TestUsername, this.Settings.TestPassword);
        }
    }
}