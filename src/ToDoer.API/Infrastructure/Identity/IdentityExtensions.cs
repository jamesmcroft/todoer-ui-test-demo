namespace ToDoer.API.Infrastructure.Identity
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using ToDoer.API.Features.Identity.Data;
    using ToDoer.API.Infrastructure.Data;

    public static class IdentityExtensions
    {
        public static IServiceCollection AddAuthenticatedUserAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthenticatedUserAccessor, AuthenticatedUserAccessor>();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(
                options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength = 8;
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
