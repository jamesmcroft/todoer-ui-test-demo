namespace ToDoer.API.Features.Identity.Data
{
    using System;
    using MADE.Data.EFCore;

    public class UserPermission : EntityBase
    {
        public User User { get; set; }

        public Guid UserId { get; set; }

        public Permission Permission { get; set; }
    }
}