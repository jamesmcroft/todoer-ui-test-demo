namespace ToDoer.API.Infrastructure.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.EFCore.Converters;
    using MADE.Data.EFCore.Extensions;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ToDoer.API.Features.Identity.Data;

    public class AppDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }

        public override DbSet<Role> Roles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public override DbSet<RoleClaim> RoleClaims { get; set; }

        public override DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public override DbSet<UserClaim> UserClaims { get; set; }

        public override DbSet<UserLogin> UserLogins { get; set; }

        public override DbSet<UserToken> UserTokens { get; set; }

        public static AppDbContext CreateContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            return new AppDbContext(options);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetEntityDates();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.SetEntityDates();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.ApplyUtcDateTimeConverter();
        }
    }
}