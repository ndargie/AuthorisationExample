using Microsoft.EntityFrameworkCore;
using Zetes.RestService.Entities;

namespace Zetes.RestService.Mapping.Entity
{
    public static class SupplierMappingOverride
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().Property(s => s.Website).HasMaxLength(200);
            modelBuilder.Entity<Supplier>().Property(s => s.Name).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<Supplier>().Property(s => s.ContactNumber).HasMaxLength(20);
        }
    }
}
