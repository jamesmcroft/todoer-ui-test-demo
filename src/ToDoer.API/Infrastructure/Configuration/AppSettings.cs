namespace ToDoer.API.Infrastructure.Configuration
{
    using System;
    using MADE.Data.Validation.Extensions;
    using Microsoft.Extensions.Configuration;

    public class AppSettings : IAppSettings
    {
        public const string Section = "AppSettings";

        protected const string SqlConnectionStringKey = "SqlConnectionString";

        protected const string BaseAppUrlKey = "BaseAppUrl";

        public AppSettings(string sqlConnectionString, string baseAppUrl)
        {
            SqlConnectionString = sqlConnectionString.IsNullOrWhiteSpace()
                ? throw new ArgumentNullException(nameof(sqlConnectionString))
                : sqlConnectionString;
            BaseAppUrl = baseAppUrl.IsNullOrWhiteSpace()
                ? throw new ArgumentNullException(nameof(baseAppUrl))
                : baseAppUrl;
        }

        public string BaseAppUrl { get; }

        public string SqlConnectionString { get; }

        public static AppSettings Build(IConfiguration configuration)
        {
            var section = configuration.GetSection(Section);
            return new(section[SqlConnectionStringKey], section[BaseAppUrlKey]);
        }
    }
}