using Microsoft.EntityFrameworkCore;
using Zetes.RestService.Entities;

namespace Zetes.RestService.Mapping.Entity
{
    public static class CompanyMappingOverrides
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasMany(c => c.Users);
            modelBuilder.Entity<Company>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>().HasMany(c => c.Customers);
        }
    }
}
