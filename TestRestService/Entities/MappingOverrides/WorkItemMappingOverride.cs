using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestService.Entities.MappingOverrides
{
    public static class WorkItemMappingOverride
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>().Property(p => p.OrderNumber).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<WorkItem>().Property(p => p.Description).HasMaxLength(400);
            modelBuilder.Entity<WorkItem>().Property(p => p.QuoteRaised).IsRequired(false);
            modelBuilder.Entity<WorkItem>().Property(p => p.Started).IsRequired(false);
            modelBuilder.Entity<WorkItem>().Property(p => p.Finished).IsRequired(false);
            modelBuilder.Entity<WorkItem>().Property(p => p.InvoiceRaised).IsRequired(false);
            modelBuilder.Entity<WorkItem>().Property(p => p.InvoicePaid).IsRequired(false);
            modelBuilder.Entity<WorkItem>().Property(p => p.Quote).HasColumnType("decimal(18,2)");
        }
    }
}
