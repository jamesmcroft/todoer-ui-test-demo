namespace ToDoer.API.Features.Identity.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<RolePermission> PermissionPresets { get; set; } = new List<RolePermission>();
    }
}