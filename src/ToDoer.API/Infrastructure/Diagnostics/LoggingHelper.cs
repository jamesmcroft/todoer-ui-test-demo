namespace ToDoer.API.Infrastructure.Diagnostics
{
    using Serilog;
    using Serilog.Events;
    using Serilog.Exceptions;
    using Serilog.Sinks.SystemConsole.Themes;

    public static class LoggingHelper
    {
        private const string LoggingMessageTemplate = "[{Timestamp:HH:mm:ss} {CorrelationId} {Level}] {SourceContext}{NewLine}<{MachineName}> - Thread: {ThreadId}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";

        public static void ConfigureApplicationLogging(string applicationName, bool isDebug, string environment, bool logToFile = false, string logFilePath = "Logs/")
        {
            var configuration = new LoggerConfiguration();

            if (isDebug)
            {
                configuration = configuration
                               .MinimumLevel.Debug()
                               .WriteTo.Console(
                                   outputTemplate: LoggingMessageTemplate,
                                   theme: AnsiConsoleTheme.Literate,
                                   restrictedToMinimumLevel: LogEventLevel.Information);
            }
            else
            {
                configuration = configuration
                               .MinimumLevel.Information()
                               .WriteTo.Console(
                                   outputTemplate: LoggingMessageTemplate,
                                   theme: AnsiConsoleTheme.Literate,
                                   restrictedToMinimumLevel: LogEventLevel.Information);
            }

            if (isDebug || logToFile)
            {
                configuration.WriteTo.File($"{logFilePath}log_.log", rollingInterval: RollingInterval.Day, outputTemplate: LoggingMessageTemplate);
            }

            Log.Logger = configuration
                        .Enrich.FromLogContext()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("System", LogEventLevel.Warning)
                        .Enrich.WithProperty("ApplicationName", applicationName)
                        .Enrich.WithProperty("Environment", environment)
                        .Enrich.WithCorrelationId()
                        .Enrich.WithMachineName()
                        .Enrich.WithThreadId()
                        .Enrich.WithExceptionDetails()
                        .CreateLogger();

            Log.Information("{ApplicationName} - Starting host...", applicationName);
        }
    }
}
