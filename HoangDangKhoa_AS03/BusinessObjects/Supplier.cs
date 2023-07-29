using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }
        [Required, StringLength(40)]
        public string SupplierName { get; set; }
        [Required, StringLength(255)]
        public string SupplierAddress { get; set; }
        [Required, StringLength(12)]
        public string Telephone { get; set; }

        [JsonIgnore]
        public virtual ICollection<FlowerBouquet> FlowerBouquets { get; set; }
    }
}
