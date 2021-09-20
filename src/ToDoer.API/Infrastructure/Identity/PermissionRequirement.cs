namespace ToDoer.API.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Authorization;
    using ToDoer.API.Features.Identity.Data;

    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission permission)
        {
            this.Permission = permission;
        }

        public Permission Permission { get; }
    }
}
