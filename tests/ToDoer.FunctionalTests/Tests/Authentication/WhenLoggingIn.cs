namespace ToDoer.FunctionalTests.Tests.Authentication
{
    using System.Net;
    using System.Threading.Tasks;
    using Flurl.Http;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using Shouldly;
    using ToDoer.API.Features.Authentication.Domain.Login;
    using ToDoer.FunctionalTests.Infrastructure;

    [TestFixture]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class WhenLoggingIn : FunctionalTestFixture
    {
        private const string Endpoint = "/api/auth/login";

        [Test]
        public async Task ShouldLoginWithValidCredentials()
        {
            // Arrange
            var client = AppFactory
                .GetFlurlClient()
                .WithHeader("ApiVersion", "1.0");

            var loginRequest = this.ConstructLoginRequestFromConfig();

            // Act
            var response = await client.Request(Endpoint)
                .AllowAnyHttpStatus()
                .PostJsonAsync(loginRequest);

            // Assert
            response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        }

        [TestCase("NotValidEmail@test.com", "IncorrectPassword")]
        [TestCase("james.croft@razor.co.uk", "IncorrectPassword")]
        [TestCase("", "")]
        public async Task ShouldRejectLoginWithIncorrectCredentials(string email, string password)
        {
            // Arrange
            var client = AppFactory
                .GetFlurlClient()
                .WithHeader("ApiVersion", "1.0");

            var loginRequest = new LoginRequestDto { Email = email, Password = password };

            // Act
            var response = await client.Request(Endpoint)
                .AllowAnyHttpStatus()
                .PostJsonAsync(loginRequest);

            // Assert
            response.StatusCode.ShouldBe((int)HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task ShouldRejectLoginIfUserDisabled()
        {
            // Arrange
            var user = await this.CreateNewUserAsync(isDisabled: true);

            var client = AppFactory
                .GetFlurlClient()
                .WithHeader("ApiVersion", "1.0");

            var loginRequest = new LoginRequestDto { Email = user.Email, Password = user.Password };

            // Act
            var response = await client.Request(Endpoint)
                .AllowAnyHttpStatus()
                .PostJsonAsync(loginRequest);

            // Assert
            response.StatusCode.ShouldBe((int)HttpStatusCode.Unauthorized);
        }

        private LoginRequestDto ConstructLoginRequestFromConfig()
        {
            var config = this.GetScopedService<IConfiguration>();
            IConfigurationSection testUserConfig = config.GetSection("TestUser");
            string email = testUserConfig["Email"];
            string password = testUserConfig["Password"];
            return new LoginRequestDto { Email = email, Password = password };
        }
    }
}