namespace ToDoer.API.Infrastructure.Configuration
{
    public interface IAppSettings
    {
        string SqlConnectionString { get; }
    }
}
