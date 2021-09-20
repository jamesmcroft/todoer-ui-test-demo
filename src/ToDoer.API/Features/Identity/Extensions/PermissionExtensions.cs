namespace ToDoer.API.Features.Identity.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MADE.Data.Validation.Extensions;
    using ToDoer.API.Features.Identity.Data;

    public static class PermissionExtensions
    {
        public static IEnumerable<Permission> GetPermissions()
        {
            return Enum.GetValues(typeof(Permission))
                .Cast<Permission>()
                .Where(permission => permission != Permission.None);
        }

        public static Permission ToPermission(string permission)
        {
            return permission.IsNullOrWhiteSpace()
                ? Permission.None
                : Enum.TryParse(permission, out Permission returnValue)
                ? returnValue
                : Permission.None;
        }
    }
}