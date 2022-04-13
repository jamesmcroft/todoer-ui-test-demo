namespace ToDoer.FunctionalTests.Infrastructure
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Respawn;
    using Respawn.Graph;
    using ToDoer.API.Features.Identity.Data;
    using ToDoer.API.Infrastructure.Configuration;
    using ToDoer.API.Infrastructure.Data;
    using ToDoer.FunctionalTests.Fakers;
    using ToDoer.FunctionalTests.Infrastructure.Authentication;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public abstract class FunctionalTestFixture
    {
        private IConfigurationRoot configurationRoot;
        private Checkpoint databaseCheckpoint;

        public AppSettings Settings { get; private set; }

        public FunctionalTestApplicationFactory AppFactory { get; protected set; }

        public IServiceScope DiServiceScope { get; private set; }

        public AppDbContext DbContext { get; private set; }

        public static IConfigurationBuilder SetupConfigurationBuilder(IConfigurationBuilder configurationBuilder)
        {
            return configurationBuilder
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Test.json"))
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Local.json"), optional: true);
        }

        [SetUp]
        public virtual Task Setup()
        {
            this.configurationRoot = GetConfigurationRoot();
            this.Settings = AppSettings.Build(this.configurationRoot);

            this.AppFactory = new FunctionalTestApplicationFactory(SetupConfigurationBuilder);
            this.DiServiceScope = this.AppFactory.Services.CreateScope();

            this.DbContext = this.GetScopedService<AppDbContext>();
            this.DbContext.Database.EnsureCreated();

            this.databaseCheckpoint = new Checkpoint
            {
                TablesToIgnore = new Table[]
                {
                    new("__EFMigrationsHistory"),
                    new ("AspNetRoles"),
                    new("RolesPermissions"),
                    new("UserPermissions")
                },
                DbAdapter = DbAdapter.SqlServer
            };

            return Task.CompletedTask;
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.Reset();
        }

        protected virtual void Reset()
        {
            this.databaseCheckpoint.Reset(this.Settings.SqlConnectionString).Wait();
            this.DbContext?.Dispose();
            this.DiServiceScope?.Dispose();
            this.AppFactory?.Dispose();
        }

        public async Task<TestUser> CreateNewUserAsync(
            string email = "user@test.com",
            string password = "1984Bbiwy!",
            bool isDisabled = false,
            params UserPermission[] permissions)
        {
            User user = UserFaker.User(email, isDisabled).Generate();
            var userManager = this.GetScopedService<UserManager<User>>();
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var message = string.Join(" <> ", result.Errors.Select(x => x.Description));
                throw new Exception($"Could not create test user account. {message}");
            }

            return new TestUser(user, password, permissions);
        }

        protected static IConfigurationRoot GetConfigurationRoot()
        {
            return SetupConfigurationBuilder(new ConfigurationBuilder()).Build();
        }

        protected T GetScopedService<T>()
        {
            return this.AppFactory != null ? this.AppFactory.GetScopedService<T>() : default;
        }
    }
}