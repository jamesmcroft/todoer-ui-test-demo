namespace ToDoer.API.Features.Identity.Data
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }

        public override Guid UserId { get; set; }

        public virtual Role Role { get; set; }

        public override Guid RoleId { get; set; }
    }
}