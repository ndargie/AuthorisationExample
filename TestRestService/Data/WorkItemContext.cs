using Microsoft.EntityFrameworkCore;
using TestRestService.Entities;
using TestRestService.Entities.MappingOverrides;

namespace TestRestService.Data
{
    public class WorkItemContext : DbContext
    {
        public WorkItemContext(DbContextOptions<WorkItemContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            WorkItemMappingOverride.Map(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WorkItem> WorkItems { get; set; }
    }
}
