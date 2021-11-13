namespace ToDoer.UITests.Infrastructure.Tests
{
    using System;
    using System.Collections.Generic;
    using Legerity;
    using Legerity.Web;
    using MADE.Data.Validation.Extensions;
    using Microsoft.Edge.SeleniumTools;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using ToDoer.UITests.Infrastructure.Configuration;
    using ToDoer.UITests.Pages;
    using ToDoer.UITests.Pages.Authentication;

    public class BaseTestFixture
    {
        public static TimeSpan ImplicitWait => TimeSpan.FromSeconds(5);

        protected BaseTestFixture(AppManagerOptions options)
        {
            Options = options;
        }

        public static IEnumerable<AppManagerOptions> TestPlatformOptions => new List<AppManagerOptions>
        {
            new WebAppManagerOptions(WebAppDriverType.Chrome, Environment.CurrentDirectory)
            {
                Url = "https://localhost:3000/", Maximize = true, ImplicitWait = ImplicitWait
            },
            new WebAppManagerOptions(WebAppDriverType.EdgeChromium, Environment.CurrentDirectory)
            {
                Url = "https://localhost:3000/", Maximize = true, ImplicitWait = ImplicitWait
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

            switch (Options)
            {
                case WebAppManagerOptions webOptions:
                    webOptions.DriverOptions = webOptions.DriverType switch
                    {
                        WebAppDriverType.Chrome => GetChromeOptions(this.Settings),
                        _ => webOptions.DriverOptions
                    };

                    break;
            }

            AppManager.StartApp(Options);

            this.Login();
        }

        private static DriverOptions GetEdgeChromiumOptions(UITestSettings settings)
        {
            var options = new EdgeOptions();

            if (!settings.BrowserMode.IsNullOrWhiteSpace())
            {
                options.AddArguments(settings.BrowserMode);
            }

            return options;
        }

        private static DriverOptions GetChromeOptions(UITestSettings settings)
        {
            var options = new ChromeOptions();

            if (!settings.BrowserMode.IsNullOrWhiteSpace())
            {
                options.AddArguments(settings.BrowserMode);
            }

            return options;
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