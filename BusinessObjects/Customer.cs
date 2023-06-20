using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class Customer : IdentityUser
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime Birthday { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
