namespace ToDoer.API.Features.Tasks.Data.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ToDoer.API.Infrastructure.Data;

    public class TaskListMapping : IEntityTypeConfiguration<TaskList>
    {
        public void Configure(EntityTypeBuilder<TaskList> builder)
        {
            builder.ToTable("TaskLists");
            builder.ConfigureUserGeneratedEntity();

            builder.Property(list => list.Name).IsRequired();

            builder.HasOne(x => x.AssignedTo)
                .WithMany(x => x.TaskLists)
                .HasForeignKey(x => x.AssignedToId);

            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.List)
                .HasForeignKey(x => x.TaskListId);
        }
    }
}