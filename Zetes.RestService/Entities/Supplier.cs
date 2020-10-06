using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zetes.RestService.Entities
{
    public class Supplier : BaseEntity
    {
        [MaxLength(200)]
        public string Website { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string ContactNumber { get; set; }
    }
}
