using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zetes.Dtos.Enums;

namespace Zetes.RestService.Entities.Views
{
    [Table("ViewWorkItem")]
    public class ViewWorkItem : BaseEntity
    {
        [Key]
        public new int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Customer { get; set; }
        public string User { get; set; }
        public decimal Quote { get; set; }
        public WorkItemStatus Status { get; set; }
    }
}
