namespace ToDoer.FunctionalTests.Fakers
{
    using Bogus;
    using ToDoer.API.Features.Tasks.Domain.AddTask;
    using ToDoer.API.Features.Tasks.Domain.AddTaskList;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class TaskFaker
    {
        public static Faker<AddTaskListRequestDto> AddTaskListRequest()
        {
            return new Faker<AddTaskListRequestDto>()
                .RuleFor(x => x.Name, faker => faker.Commerce.ProductName());
        }

        public static Faker<AddTaskRequestDto> AddTaskRequest()
        {
            return new Faker<AddTaskRequestDto>()
                .RuleFor(x => x.Name, faker => faker.Commerce.ProductDescription());
        }
    }
}