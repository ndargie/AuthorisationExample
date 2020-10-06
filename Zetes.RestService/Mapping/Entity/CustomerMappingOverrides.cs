using Microsoft.EntityFrameworkCore;
using Zetes.RestService.Entities;

namespace Zetes.RestService.Mapping.Entity
{
    public static class CustomerMappingOverrides
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.Email).HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.Phone).HasMaxLength(20);
        }
    }
}
