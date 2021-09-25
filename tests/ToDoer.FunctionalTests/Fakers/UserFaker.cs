namespace ToDoer.FunctionalTests.Fakers
{
    using Bogus;
    using ToDoer.API.Features.Identity.Data;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class UserFaker
    {
        public static Faker<User> User(string email = default, bool isDisabled = false)
        {
            return new Faker<User>()
                .RuleFor(x => x.Id, faker => faker.Random.Guid())
                .RuleFor(x => x.FirstName, faker => faker.Person.FirstName)
                .RuleFor(x => x.LastName, faker => faker.Person.LastName)
                .RuleFor(x => x.Email, faker => email ?? faker.Person.Email)
                .RuleFor(x => x.UserName, (faker, user) => user.Email ?? faker.Person.Email)
                .RuleFor(x => x.IsDeleted, isDisabled);
        }
    }
}