using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class OrderItemRequest
    {
        [Required]
        public FlowerBouquet FlowerBouquet { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
