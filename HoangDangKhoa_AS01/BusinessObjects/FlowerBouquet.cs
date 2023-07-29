using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class FlowerBouquet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlowerBouquetID { get; set; }
        [Required, StringLength(40)]
        public string FlowerBouquetName { get; set; }
        [Required, StringLength(40)]
        public string Description { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int UnitPrice { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int UnitsInStock { get; set; }
        public int FlowerBouquetStatus { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
