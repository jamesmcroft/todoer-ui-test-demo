namespace ToDoer.FunctionalTests.Tests.Authentication
{
    using System.Net;
    using System.Threading.Tasks;
    using Flurl.Http;
    using NUnit.Framework;
    using Shouldly;
    using ToDoer.FunctionalTests.Infrastructure;

    [TestFixture]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class WhenLoggingOut : FunctionalTestFixture
    {
        private const string Endpoint = "/api/auth/logout";

        [Test]
        public async Task ShouldLogoutIfAuthenticated()
        {
            // Arrange
            var user = await this.CreateNewUserAsync();

            var client = AppFactory
                .GetFlurlClient(impersonateAs: user.AsImpersonationOptions())
                .WithHeader("ApiVersion", "1.0");

            // Act
            var response = await client.Request(Endpoint)
                .AllowAnyHttpStatus()
                .PostJsonAsync(new { });

            // Assert
            response.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldRejectLogoutIfNotAuthenticated()
        {
            // Arrange
            var client = AppFactory
                .GetFlurlClient()
                .WithHeader("ApiVersion", "1.0");

            // Act
            var response = await client.Request(Endpoint)
                .AllowAnyHttpStatus()
                .PostJsonAsync(new { });

            // Assert
            response.StatusCode.ShouldBe((int)HttpStatusCode.Unauthorized);
        }
    }
}