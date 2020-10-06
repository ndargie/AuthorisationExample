using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zetes.RestService.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public Company Company { get; set; }

        public User()
        {
            WorkItems = new List<WorkItem>();
        }
    }
}
