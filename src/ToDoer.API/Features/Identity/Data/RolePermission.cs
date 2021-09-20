namespace ToDoer.API.Features.Identity.Data
{
    using System;
    using MADE.Data.EFCore;

    public class RolePermission : EntityBase
    {
        public Role Role { get; set; }

        public Guid RoleId { get; set; }

        public Permission Permission { get; set; }
    }
}
