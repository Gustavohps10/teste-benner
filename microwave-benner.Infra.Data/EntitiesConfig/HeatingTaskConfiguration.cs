using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using microwave_benner.Domain.Entities;

namespace microwave_benner.Infra.Data.EntitiesConfig
{
    internal class HeatingTaskConfiguration : IEntityTypeConfiguration<HeatingTask>
    {
        public void Configure(EntityTypeBuilder<HeatingTask> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.time).IsRequired();
            builder.Property(x => x.power).IsRequired();
            builder.Property(x => x.startTime).IsRequired();
            builder.Property(x => x.pauseTime).IsRequired(false);
            builder.Property(x => x.endTime).IsRequired(false);

            builder.ToTable("heatingTasks");
        }
    }
}
