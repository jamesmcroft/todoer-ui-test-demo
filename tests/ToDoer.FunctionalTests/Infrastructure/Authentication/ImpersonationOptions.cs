namespace ToDoer.FunctionalTests.Infrastructure.Authentication
{
    using System;
    using System.Collections.Generic;
    using ToDoer.API.Features.Identity.Data;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ImpersonationOptions
    {
        public Guid? ImpersonateAsId { get; set; }

        public string ImpersonateAsName { get; set; }

        public string ImpersonateAsEmail { get; set; }

        public IEnumerable<UserPermission> ImpersonateAsPermissions { get; set; }
    }
}