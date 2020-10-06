using Microsoft.EntityFrameworkCore;
using Zetes.RestService.Entities;
using Zetes.RestService.Entities.Views;
using Zetes.RestService.Mapping.Entity;

namespace Zetes.RestService.Data
{
    public class WorkContext : DbContext
    {
        public WorkContext(DbContextOptions<WorkContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CompanyMappingOverrides.Map(modelBuilder);
            CustomerMappingOverrides.Map(modelBuilder);
            UserMappingOverride.Map(modelBuilder);
            MaterialMappingOverride.Map(modelBuilder);
            SupplierMappingOverride.Map(modelBuilder);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MaterialCost> MaterialCosts { get; set; }
        public DbSet<ViewWorkItem> ViewWorkItems { get; set; }
    }
}
