using System.ComponentModel.DataAnnotations;

namespace FlowerBouquetWebAPI.Models
{
    public class PostFlowerBouquet
    {
        [Required, StringLength(40)]
        public string FlowerBouquetName { get; set; }
        [Required, StringLength(40)]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UnitPrice { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int UnitsInStock { get; set; }
        [Required]
        public int FlowerBouquetStatus { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int SupplierID { get; set; }
    }
}
