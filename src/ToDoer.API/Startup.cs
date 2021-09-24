namespace ToDoer.API
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MADE.Web.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using ToDoer.API.Features.Identity.Data.Seeders;
    using ToDoer.API.Infrastructure.API;
    using ToDoer.API.Infrastructure.Configuration;
    using ToDoer.API.Infrastructure.Data;
    using ToDoer.API.Infrastructure.Data.Seeders;
    using ToDoer.API.Infrastructure.Data.Serialization;
    using ToDoer.API.Infrastructure.Exceptions;
    using ToDoer.API.Infrastructure.Identity;

    public class Startup
    {
        private const string AllowOrigins = "_allowOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;

            IsTesting = Configuration["IsTesting"] == "true";
            IsDevelopment = HostEnvironment.IsDevelopment();
        }

        private IConfiguration Configuration { get; }

        private IWebHostEnvironment HostEnvironment { get; }

        private bool IsTesting { get; }

        private bool IsDevelopment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = AppSettings.Build(Configuration);
            services.AddTransient<IAppSettings, AppSettings>(_ => appSettings);

            services.AddAuthenticatedUserAccessor();
            services.AddMemoryCache();

            services.AddControllers();

            services.AddAppDbContext(appSettings.SqlConnectionString, !IsDevelopment);
            services.AddIdentity();

            services.AddMvc();

            services.AddMediatR(typeof(Startup))
                .AddExceptionHandlers()
                .AddSwaggerGenerator("ToDoer");

            services.AddDataSeeder<TestUserSeeder>();

            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                .AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.Cookie.Name = ".ToDoerAuth";
                        options.ExpireTimeSpan = TimeSpan.FromHours(12);
                        options.SlidingExpiration = true;
                        options.Cookie.HttpOnly = false;
                        options.Cookie.SameSite = SameSiteMode.Strict;
                        options.Events.OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        };
                        options.Events.OnRedirectToAccessDenied = context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        };
                    });

            services.AddMvcCore(
                    options =>
                    {
                        AuthorizationPolicy
                            policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                        options.Filters.Add(new AuthorizeFilter(policy));
                        options.EnableEndpointRouting = false;
                    })
                .AddNewtonsoftJson(options => JsonConstants.ApplyTo(options.SerializerSettings))
                .AddAuthorization(options =>
                {
                    options.DefaultPolicy =
                        new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser().Build();
                    options.AddPermissionPolicies();
                })
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            services.AddApiVersionHeaderSupport();

            services.AddCors(o => o.AddPolicy(
                AllowOrigins,
                builder => builder
                    .WithOrigins(appSettings.BaseAppUrl)
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI("ToDoer", apiVersionProvider);
            }

            app.UseHttpContextExceptionHandling();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(AllowOrigins);
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context?.Database.Migrate();

            foreach (IDataSeeder seeder in serviceScope.ServiceProvider.GetRequiredService<IEnumerable<IDataSeeder>>())
            {
                seeder?.SeedAsync().Wait();
            }
        }
    }
}