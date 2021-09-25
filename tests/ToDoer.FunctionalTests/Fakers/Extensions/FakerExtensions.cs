namespace ToDoer.FunctionalTests.Fakers.Extensions
{
    using Bogus;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class FakerExtensions
    {
        public static double RandomDouble(this Faker faker, double minimum, double maximum)
        {
            return (faker.Random.Double() * (maximum - minimum)) + minimum;
        }

        public static decimal RandomDecimal(this Faker faker, decimal minimum, decimal maximum)
        {
            return (faker.Random.Decimal() * (maximum - minimum)) + minimum;
        }
    }
}