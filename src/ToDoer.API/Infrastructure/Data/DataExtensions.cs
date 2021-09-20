namespace ToDoer.API.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Features.Identity.Data;
    using Identity;
    using MADE.Data.EFCore;
    using MADE.Data.EFCore.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.Extensions.DependencyInjection;

    public static class DataExtensions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, string sqlConnectionString,
            bool isProduction = false)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                if (!isProduction)
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }

                options.UseSqlServer(sqlConnectionString);
            });

            return services;
        }

        public static EntityTypeBuilder<TEntity> ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : EntityBase
        {
            builder.HasKey(x => x.Id);
            builder.ConfigureDateProperties();

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureUserGeneratedEntity<TEntity>(
            this EntityTypeBuilder<TEntity> builder)
            where TEntity : UserGeneratedEntityBase
        {
            builder.ConfigureEntity();
            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.UpdatedById)
                .OnDelete(DeleteBehavior.NoAction);

            return builder;
        }

        public static void SetEntityUser(this DbContext dbContext, AuthenticatedUser authenticatedUser)
        {
            IEnumerable<EntityEntry> entries = dbContext.ChangeTracker
                .Entries()
                .Where(
                    entry => entry.Entity is UserGeneratedEntityBase &&
                             entry.State is EntityState.Added or EntityState.Modified);

            foreach (EntityEntry entry in entries)
            {
                var entity = (UserGeneratedEntityBase)entry.Entity;

                entity.UpdatedById = authenticatedUser.Id;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedById = authenticatedUser.Id;
                }
            }
        }
    }
}