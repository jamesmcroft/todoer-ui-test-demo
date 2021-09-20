namespace ToDoer.API.Features.Identity.Data
{
    using System;
    using System.Collections.Generic;
    using MADE.Data.Validation.Extensions;
    using Microsoft.AspNetCore.Identity;
    using ToDoer.API.Features.Tasks.Data;

    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool AcceptTermsAndConditions { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

        public string Name => $"{this.FirstName} {this.LastName}";

        public string DisplayName => this.Name.IsNullOrWhiteSpace() ? this.Email : this.Name;

        public ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();

        public override string ToString()
        {
            return this.DisplayName == this.Email ? this.Email : $"{this.DisplayName} ({this.Email})";
        }
    }
}