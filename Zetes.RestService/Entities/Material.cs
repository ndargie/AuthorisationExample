using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zetes.RestService.Entities
{
    public class Material : BaseEntity
    {
        [Required]
        public Supplier Supplier { get; set; }
        [Required]
        [MaxLength(50)]
        public string UniqueIdentifier { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public ICollection<MaterialCost> Costs { get; set; }

        public Material()
        {
            Costs = new List<MaterialCost>();
        }
    }
}
