namespace ToDoer.API.Features.Identity.Data.Seeders
{
    using System.Threading.Tasks;
    using MADE.Data.Validation.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using ToDoer.API.Features.Tasks.Data;
    using ToDoer.API.Infrastructure.Data;
    using ToDoer.API.Infrastructure.Data.Seeders;

    public class TestUserSeeder : IDataSeeder
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly AppDbContext dbContext;

        public TestUserSeeder(UserManager<User> userManager, AppDbContext dbContext, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task SeedAsync()
        {
            IConfigurationSection testUserConfig = this.configuration.GetSection("TestUser");

            string email = testUserConfig["Email"];
            string password = testUserConfig["Password"];

            if (email.IsNullOrWhiteSpace() || password.IsNullOrWhiteSpace())
            {
                return;
            }

            await this.InitializeUserAsync(email, password);
        }

        private async Task InitializeUserAsync(string email, string password)
        {
            User user = await this.userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Test",
                    LastName = "User",
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    AcceptTermsAndConditions = true
                };

                var result = await this.userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var taskList = new TaskList
                    {
                        AssignedToId = user.Id, Name = "My Tasks", CreatedById = user.Id, UpdatedById = user.Id
                    };

                    await this.dbContext.AddAsync(taskList);
                    await this.dbContext.SaveChangesAsync();
                }
            }
        }
    }
}