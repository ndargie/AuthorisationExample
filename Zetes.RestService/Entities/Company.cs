using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zetes.RestService.Entities
{
    public class Company : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<User> Users { get; set;}
        public ICollection<Customer> Customers { get; set; }

        public Company()
        {
            Users = new List<User>();
            Customers = new List<Customer>();
        }
    }
}
