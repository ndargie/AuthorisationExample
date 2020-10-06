using Microsoft.EntityFrameworkCore;
using Zetes.RestService.Entities;

namespace Zetes.RestService.Mapping.Entity
{
    public static class MaterialMappingOverride
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().Property(m => m.UniqueIdentifier).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Material>().Property(m => m.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Material>().Property(m => m.Description).HasMaxLength(250);
            modelBuilder.Entity<Material>().HasMany(m => m.Costs);
        }
    }
}
