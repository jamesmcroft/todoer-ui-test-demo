namespace ToDoer.API.Infrastructure.Identity
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Security.Claims;
    using ToDoer.API.Features.Identity.Data;

    public class AuthenticatedUser
    {
        public AuthenticatedUser(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;
            string subject = claimsPrincipal.Claims.SingleOrDefault(claim => claim.Type == IdentityConstants.UserIdClaim)?.Value;
            string emailAddress = claimsPrincipal.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            if (subject == null || emailAddress == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal), "Authenticated user details could not be found.");
            }

            Id = Guid.Parse(subject);
            EmailAddress = emailAddress;
            Claims = claimsPrincipal.Claims.ToImmutableList();

            Permissions = this.Claims.Where(x => x.Type == IdentityConstants.PermissionClaim)
                .Where(x => Enum.IsDefined(typeof(Permission), x.Value))
                .Select(x => Enum.Parse<Permission>(x.Value))
                .ToImmutableList();
        }

        public Guid Id { get; }

        public ClaimsPrincipal ClaimsPrincipal { get; }

        public string EmailAddress { get; }

        public IImmutableList<Permission> Permissions { get; }

        public IImmutableList<Claim> Claims { get; }

        public bool HasPermission(Permission permission)
        {
            return Permissions.Any(x => x == permission);
        }
    }
}