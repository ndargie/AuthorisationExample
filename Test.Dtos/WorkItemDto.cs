using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Dtos
{
    public class WorkItemDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public decimal Quote { get; set; }
        public DateTime? QuoteRaised { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }

        public DateTime? InvoiceRaised { get; set; }
        public DateTime? InvoicePaid { get; set; }
    }
}
