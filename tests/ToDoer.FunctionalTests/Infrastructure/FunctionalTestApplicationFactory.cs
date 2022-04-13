namespace ToDoer.FunctionalTests.Infrastructure
{
    using System;
    using Flurl.Http;
    using MADE.Data.Validation.Extensions;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using ToDoer.API;
    using ToDoer.API.Infrastructure.Diagnostics;
    using ToDoer.FunctionalTests.Infrastructure.Authentication;

    /// <summary>
    /// Application pipeline factory, facilitating the bootstrapping of a fully working application pipeline
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class FunctionalTestApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly Func<IConfigurationBuilder, IConfigurationBuilder> configSetup;

        private readonly Action<IServiceCollection> configServices;

        public FunctionalTestApplicationFactory(
            Func<IConfigurationBuilder, IConfigurationBuilder> configSetup,
            Action<IServiceCollection> configServices = default)
        {
            this.configSetup = configSetup;
            this.configServices = configServices;
            LoggingHelper.ConfigureApplicationLogging("Functional Tests", true, "Test");
        }

        public virtual FlurlClient GetFlurlClient(bool authenticated = false, ImpersonationOptions impersonateAs = null)
        {
            if (authenticated || impersonateAs != null)
            {
                return new FlurlClient(
                    this.WithWebHostBuilder(
                            builder =>
                            {
                                builder.ConfigureServices(
                                    services =>
                                    {
                                        services.Configure<ImpersonationOptions>(
                                            options =>
                                            {
                                                if (impersonateAs.ImpersonateAsId.HasValue)
                                                {
                                                    options.ImpersonateAsId = impersonateAs.ImpersonateAsId;
                                                }

                                                if (!impersonateAs.ImpersonateAsName.IsNullOrWhiteSpace())
                                                {
                                                    options.ImpersonateAsName = impersonateAs.ImpersonateAsName;
                                                }

                                                if (!impersonateAs.ImpersonateAsEmail.IsNullOrWhiteSpace())
                                                {
                                                    options.ImpersonateAsEmail = impersonateAs.ImpersonateAsEmail;
                                                }

                                                if (impersonateAs.ImpersonateAsPermissions != null)
                                                {
                                                    options.ImpersonateAsPermissions =
                                                        impersonateAs.ImpersonateAsPermissions;
                                                }
                                            });

                                        services.AddMvcCore()
                                            .AddAuthorization(options => options.DefaultPolicy =
                                                    new AuthorizationPolicyBuilder(TestAuthenticationHandler
                                                            .TestAuthenticationScheme)
                                                        .RequireAuthenticatedUser()
                                                        .Build());

                                        services.AddAuthentication(
                                                options =>
                                                {
                                                    options.DefaultScheme = TestAuthenticationHandler
                                                        .TestAuthenticationScheme;
                                                    options.DefaultAuthenticateScheme = TestAuthenticationHandler
                                                        .TestAuthenticationScheme;
                                                    options.DefaultChallengeScheme = TestAuthenticationHandler
                                                        .TestAuthenticationScheme;
                                                    options.DefaultForbidScheme = TestAuthenticationHandler
                                                        .TestAuthenticationScheme;
                                                    options.DefaultSignInScheme = TestAuthenticationHandler
                                                        .TestAuthenticationScheme;
                                                    options.DefaultSignOutScheme = TestAuthenticationHandler
                                                        .TestAuthenticationScheme;
                                                })
                                            .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                                                TestAuthenticationHandler.TestAuthenticationScheme,
                                                options => { });
                                    });
                            })
                        .CreateClient(
                            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false }));
            }

            return new FlurlClient(
                this.CreateClient(
                    new WebApplicationFactoryClientOptions { AllowAutoRedirect = false }));
        }

        public T GetScopedService<T>()
        {
            IServiceScope scope = this.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.UseSerilog();
            builder.ConfigureAppConfiguration(config => this.configSetup(config));

            builder.ConfigureServices(
                services =>
                {
                    services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
                    this.configServices?.Invoke(services);
                    services.BuildServiceProvider();
                });
        }
    }
}