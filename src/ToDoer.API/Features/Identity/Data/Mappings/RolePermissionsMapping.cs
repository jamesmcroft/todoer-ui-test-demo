namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using MADE.Data.EFCore.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ToDoer.API.Features.Identity.Extensions;

    public class RolePermissionsMapping : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rolePermission => new { rolePermission.Permission, rolePermission.RoleId });
            builder.ConfigureDateProperties();
            builder.Property(rolePermission => rolePermission.Permission)
                .HasMaxLength(50)
                .HasConversion(
                    permission => permission.ToString(),
                    s => PermissionExtensions.ToPermission(s));
        }
    }
}