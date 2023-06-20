using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class OrderDetail
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("FlowerBouquet")]
        public int FlowerBouquetID { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual FlowerBouquet FlowerBouquet { get; set; }
    }
}
