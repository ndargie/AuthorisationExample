using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zetes.Dtos.Enums;

namespace Zetes.RestService.Entities
{
    public class WorkItem : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string OrderNumber { get; set; }
        [Required]
        public decimal Quote { get; set; }
        public ICollection<Material> Materials { get; set; }
        public DateTime? QuoteRaised { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
        
        public DateTime? InvoiceRaised { get; set; }
        public DateTime? InvoicePaid { get; set; }
        [Required]
        public WorkItemStatus Status { get; set; }
        public User User { get; set; }
        public Customer Customer { get; set; }

        public WorkItem()
        {
            Status = WorkItemStatus.Raised;
            Materials = new List<Material>();
        }
    }
}
