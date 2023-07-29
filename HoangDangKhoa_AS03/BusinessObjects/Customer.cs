using Microsoft.AspNetCore.Identity;

namespace BusinessObjects
{
    public class Customer : IdentityUser
    {
        public string? CustomerName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
