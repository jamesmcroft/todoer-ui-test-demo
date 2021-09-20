namespace ToDoer.API.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Authorization;
    using ToDoer.API.Features.Identity.Data;
    using ToDoer.API.Features.Identity.Extensions;

    public static class AuthorizationOptionExtensions
    {
        public static AuthorizationOptions AddPermissionPolicies(this AuthorizationOptions options)
        {
            foreach (Permission permission in PermissionExtensions.GetPermissions())
            {
                options.AddPermissionPolicy(permission);
            }

            return options;
        }

        public static AuthorizationOptions AddPermissionPolicy(this AuthorizationOptions options, Permission permission)
        {
            options.AddPolicy(permission.ToString(), x => x.AddRequirements(new PermissionRequirement(permission)));
            return options;
        }
    }
}
