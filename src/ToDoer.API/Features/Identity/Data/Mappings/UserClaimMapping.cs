namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserClaimMapping : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("AspNetUserClaims");
            builder.HasKey(userClaim => userClaim.Id);
        }
    }
}