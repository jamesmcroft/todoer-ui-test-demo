namespace ToDoer.API.Infrastructure.Data
{
    using System;
    using MADE.Data.EFCore;
    using ToDoer.API.Features.Identity.Data;

    public abstract class UserGeneratedEntityBase : EntityBase
    {
        public User CreatedBy { get; set; }

        public User UpdatedBy { get; set; }

        public Guid? CreatedById { get; set; }

        public Guid? UpdatedById { get; set; }
    }
}