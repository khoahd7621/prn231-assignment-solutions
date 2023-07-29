using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [Required, StringLength(40)]
        public string CustomerName { get; set; }
        [Required, StringLength(40)]
        public string Email { get; set; }
        [Required, StringLength(40)]
        public string City { get; set; }
        [Required, StringLength(40)]
        public string Country { get; set; }
        [MinLength(5)]
        [MaxLength(255)]
        public string? Password { get; set; }
        [Required, StringLength(10)]
        public string Role { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
