namespace ToDoer.UITests.Infrastructure.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    public class UITestSettings
    {
        private const string TestUsernameKey = "TestUsername";
        private const string TestPasswordKey = "TestPassword";
        private const string BrowserModeKey = "BrowserMode";

        /// <summary>
        /// Initializes a new instance of the <see cref="UITestSettings"/> class with the default settings.
        /// </summary>
        public UITestSettings(
            string testUsername,
            string testPassword,
            string browserMode)
        {
            this.TestUsername = string.IsNullOrWhiteSpace(testUsername)
                ? throw new ArgumentNullException(nameof(testUsername))
                : testUsername;

            this.TestPassword = string.IsNullOrWhiteSpace(testPassword)
                ? throw new ArgumentNullException(nameof(testPassword))
                : testPassword;

            this.BrowserMode = browserMode;
        }

        public string TestUsername { get; }

        public string TestPassword { get; }

        public string BrowserMode { get; }

        /// <summary>
        /// Builds the application settings from the specified <see cref="IConfiguration"/> instance.
        /// </summary>
        /// <param name="configuration">The configuration containing the settings.</param>
        /// <returns>The constructed <see cref="UITestSettings"/>.</returns>
        public static UITestSettings Build(IConfiguration configuration)
        {
            return new UITestSettings(
                configuration[TestUsernameKey],
                configuration[TestPasswordKey],
                configuration[BrowserModeKey]);
        }
    }
}