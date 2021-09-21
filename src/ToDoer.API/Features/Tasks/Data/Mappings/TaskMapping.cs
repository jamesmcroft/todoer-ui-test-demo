namespace ToDoer.API.Features.Tasks.Data.Mappings
{
    using MADE.Data.EFCore.Converters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ToDoer.API.Infrastructure.Data;

    public class TaskMapping : IEntityTypeConfiguration<TaskListTask>
    {
        public void Configure(EntityTypeBuilder<TaskListTask> builder)
        {
            builder.ToTable("Tasks");
            builder.ConfigureUserGeneratedEntity();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DueDate).IsUtc();
            builder.Property(x => x.CompletedDate).IsUtc();
        }
    }
}