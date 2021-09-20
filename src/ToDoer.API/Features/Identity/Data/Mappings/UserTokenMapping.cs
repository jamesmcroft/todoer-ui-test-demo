namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserTokenMapping : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("AspNetUserTokens");
            builder.HasKey(userToken => new { userToken.UserId, userToken.LoginProvider, userToken.Name });
        }
    }
}