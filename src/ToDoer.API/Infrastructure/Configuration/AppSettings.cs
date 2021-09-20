namespace ToDoer.API.Infrastructure.Configuration
{
    using System;
    using MADE.Data.Validation.Extensions;
    using Microsoft.Extensions.Configuration;

    public class AppSettings : IAppSettings
    {
        public const string Section = "AppSettings";

        protected const string SqlConnectionStringKey = "SqlConnectionString";

        public AppSettings(string sqlConnectionString)
        {
            SqlConnectionString = sqlConnectionString.IsNullOrWhiteSpace() ? throw new ArgumentNullException(nameof(sqlConnectionString)) : sqlConnectionString;
        }

        public string SqlConnectionString { get; }

        public static AppSettings Build(IConfiguration configuration)
        {
            var section = configuration.GetSection(Section);
            return new(section[SqlConnectionStringKey]);
        }
    }
}
