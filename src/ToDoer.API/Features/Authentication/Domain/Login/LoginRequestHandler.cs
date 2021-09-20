namespace ToDoer.API.Features.Authentication.Domain.Login
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.Validation.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Identity.Data;
    using ToDoer.API.Infrastructure.Data;
    using IdentityConstants = ToDoer.API.Infrastructure.Identity.IdentityConstants;
    using UnauthorizedAccessException =
        ToDoer.API.Features.Authentication.Exceptions.UnauthorizedAccess.UnauthorizedAccessException;

    public class LoginRequestHandler : IRequestHandler<LoginRequest>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly AppDbContext dbContext;
        private readonly UserManager<User> userManager;

        public LoginRequestHandler(
            UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            AppDbContext dbContext)
        {
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            if (request.Email.IsNullOrWhiteSpace() || request.Password.IsNullOrWhiteSpace())
            {
                throw new UnauthorizedAccessException("No credentials provided");
            }

            User user = await this.userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Login failed");
            }

            if (user.IsDeleted)
            {
                throw new UnauthorizedAccessException("Access denied");
            }

            PasswordVerificationResult passwordVerificationResult =
                this.userManager.PasswordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Access denied");
            }

            List<UserPermission> userPermissions = await this.dbContext.UserPermissions
                .AsQueryable()
                .Where(x => x.UserId == user.Id)
                .ToListAsync(cancellationToken);

            IList<Claim> claims = await this.userManager.GetClaimsAsync(user);

            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaims(new List<Claim>
                {
                    new(IdentityConstants.UserIdClaim, user.Id.ToString("N")),
                    new(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Email, user.Email)
                }
                .Union(userPermissions.Select(
                    x => new Claim(IdentityConstants.PermissionClaim, x.Permission.ToString())))
                .Union(claims));

            await contextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    AllowRefresh = true, IsPersistent = true, IssuedUtc = DateTimeOffset.Now.ToUniversalTime()
                });

            return Unit.Value;
        }
    }
}