namespace ToDoer.API.Features.Identity.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers");
            builder.HasMany(user => user.UserRoles)
                .WithOne(userRole => userRole.User)
                .HasForeignKey(userRole => userRole.UserId)
                .IsRequired();
        }
    }
}