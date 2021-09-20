namespace ToDoer.API
{
    using System;
    using System.Globalization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using ToDoer.API.Infrastructure.Diagnostics;

    public class Program
    {
        public static int Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            bool isDevelopment = environment == Environments.Development;

            LoggingHelper.ConfigureApplicationLogging("ToDoer", isDevelopment, environment);

            try
            {
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "HomePal terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                           webBuilder.UseSerilog();
                       });
        }
    }
}
