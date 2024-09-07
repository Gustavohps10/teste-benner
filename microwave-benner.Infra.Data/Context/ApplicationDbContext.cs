using Microsoft.EntityFrameworkCore;
using microwave_benner.Domain.Entities;

namespace microwave_benner.Infra.Data.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) {}

        public DbSet<HeatingTask> HeatingTasks { get; set; }
        public DbSet<HeatingProgram> HeatingPrograms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
