namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RoleClaimMapping : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("AspNetRoleClaims");
            builder.HasKey(claim => claim.Id);
        }
    }
}