using System.ComponentModel.DataAnnotations;

namespace Zetes.RestService.Entities
{
    public class MaterialCost : BaseEntity
    {
        [Required]
        public Material Material { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}
