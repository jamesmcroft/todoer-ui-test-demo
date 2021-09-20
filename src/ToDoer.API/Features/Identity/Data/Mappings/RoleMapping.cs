namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("AspNetRoles");

            builder.HasKey(role => role.Id);
            builder.HasIndex(role => role.NormalizedName)
                .HasDatabaseName("RoleNameIndex")
                .IsUnique();

            builder.Property(role => role.ConcurrencyStamp)
                .IsConcurrencyToken();

            builder.Property(role => role.Name)
                .HasMaxLength(256);
            builder.Property(role => role.NormalizedName)
                .HasMaxLength(256);

            builder.HasMany(role => role.RoleClaims)
                .WithOne()
                .HasForeignKey(roleClaim => roleClaim.RoleId)
                .IsRequired();

            builder.HasMany(role => role.PermissionPresets)
                .WithOne(rolePermission => rolePermission.Role)
                .HasForeignKey(rolePermission => rolePermission.RoleId)
                .IsRequired();

            builder.HasMany(role => role.UserRoles)
                .WithOne(userRole => userRole.Role)
                .HasForeignKey(userRole => userRole.RoleId)
                .IsRequired();
        }
    }
}