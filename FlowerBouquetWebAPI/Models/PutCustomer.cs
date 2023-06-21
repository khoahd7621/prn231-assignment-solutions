using System.ComponentModel.DataAnnotations;

namespace FlowerBouquetWebAPI.Models
{
    public class PutCustomer
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string? Password { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
    }
}
