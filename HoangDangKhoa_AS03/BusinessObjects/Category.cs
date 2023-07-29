using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required, StringLength(40)]
        public string CategoryName { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<FlowerBouquet> FlowerBouquets { get; set; }
    }
}