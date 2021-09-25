namespace ToDoer.FunctionalTests.Infrastructure.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using ToDoer.API.Infrastructure.Identity;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string TestAuthenticationScheme = "Test";

        private readonly ImpersonationOptions impersonationOptions;

        public TestAuthenticationHandler(
            IOptions<ImpersonationOptions> impersonationOptions,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            this.impersonationOptions = impersonationOptions.Value;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>
            {
                new(IdentityConstants.UserIdClaim,
                    this.impersonationOptions.ImpersonateAsId?.ToString() ?? Guid.NewGuid().ToString()),
                new(ClaimTypes.Name, this.impersonationOptions.ImpersonateAsName),
                new(ClaimTypes.Email, this.impersonationOptions.ImpersonateAsEmail)
            };

            if (this.impersonationOptions.ImpersonateAsPermissions != null)
            {
                claims.AddRange(this.impersonationOptions.ImpersonateAsPermissions.Select(
                    x => new Claim(IdentityConstants.PermissionClaim, x.Permission.ToString("G"))));
            }

            var identity = new ClaimsIdentity(claims, TestAuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, TestAuthenticationScheme);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}