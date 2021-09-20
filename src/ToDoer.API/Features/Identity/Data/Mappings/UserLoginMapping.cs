namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserLoginMapping : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("AspNetUserLogins");
            builder.HasKey(userLogin => new { userLogin.LoginProvider, userLogin.ProviderKey });
        }
    }
}