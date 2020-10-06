using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zetes.RestService.Entities
{
    public class BaseEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        [MaxLength(200)]
        public string ModifiedBy { get; set; }       
    }
}
