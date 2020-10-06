using System;
using System.Collections.Generic;
using System.Text;
using Zetes.Dtos.Enums;

namespace Zetes.Dtos
{
    public class WorkItemDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Customer { get; set; }
        public WorkItemStatus Status { get; set; }
        public decimal Quote { get; set; }
    }
}
