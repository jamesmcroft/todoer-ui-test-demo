namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ToDoer.API.Features.Identity.Extensions;

    public class UserPermissionMapping : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(userPermission => new { userPermission.Permission, userPermission.UserId });
            builder.Property(userPermission => userPermission.Permission)
                .HasMaxLength(50)
                .HasConversion(
                    permission => permission.ToString(),
                    s => PermissionExtensions.ToPermission(s));
        }
    }
}