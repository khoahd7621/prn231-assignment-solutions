using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class CompanyProjectPutRequest
    {
        public int CompanyProjectID { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
    }
}
