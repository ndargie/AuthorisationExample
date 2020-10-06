using EntityFrameWorkHelper.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestRestService.Entities
{
    public class WorkItem : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string OrderNumber { get; set; }
        [Required]
        public decimal Quote { get; set; }
        public string Description { get; set; }
        public DateTime? QuoteRaised { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }

        public DateTime? InvoiceRaised { get; set; }
        public DateTime? InvoicePaid { get; set; }
    }
}
