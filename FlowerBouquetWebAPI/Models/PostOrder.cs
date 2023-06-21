using System.ComponentModel.DataAnnotations;

namespace FlowerBouquetWebAPI.Models
{
    public class PostOrder
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public string Freight { get; set; }
        [Required]
        public string CustomerID { get; set; }
    }
}
