using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zetes.RestService.Entities;

namespace Zetes.RestService.Mapping.Entity
{
    public static class UserMappingOverride
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.WorkItems);
            modelBuilder.Entity<User>().Property(u => u.Username)
                .IsRequired().HasMaxLength(100);
        }
    }
}
