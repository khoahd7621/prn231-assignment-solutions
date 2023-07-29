using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class CustomerRequest
    {
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
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}