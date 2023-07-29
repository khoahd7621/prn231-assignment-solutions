using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class OrderDetailRequest
    {
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int FlowerBouquetID { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Discount { get; set; }
    }
}
