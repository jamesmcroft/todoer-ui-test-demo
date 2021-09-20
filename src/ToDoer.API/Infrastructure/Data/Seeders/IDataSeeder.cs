namespace ToDoer.API.Infrastructure.Data.Seeders
{
    using System.Threading.Tasks;

    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}
