namespace ToDoer.FunctionalTests.Infrastructure.Authentication
{
    using System;
    using System.Collections.Generic;
    using ToDoer.API.Features.Identity.Data;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TestUser
    {
        public TestUser(User user, string password, params UserPermission[] permissions)
        {
            this.User = user;
            this.Password = password;
            this.Permissions = permissions;
        }

        public User User { get; set; }

        public string Password { get; set; }

        public Guid Id => this.User.Id;

        public string FirstName => this.User.FirstName;

        public string LastName => this.User.LastName;

        public string Email => this.User.Email;

        public ICollection<UserPermission> Permissions { get; set; }

        public ImpersonationOptions AsImpersonationOptions()
        {
            return new ImpersonationOptions
            {
                ImpersonateAsId = this.User.Id,
                ImpersonateAsName = $"{this.User.FirstName} {this.User.LastName}".Trim(),
                ImpersonateAsEmail = this.User.Email,
                ImpersonateAsPermissions = this.Permissions
            };
        }
    }
}