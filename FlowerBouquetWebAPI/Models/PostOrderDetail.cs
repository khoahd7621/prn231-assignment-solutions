using System.ComponentModel.DataAnnotations;

namespace FlowerBouquetWebAPI.Models
{
    public class PostOrderDetail
    {
        [Required]
        public int FlowerBouquetID { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
