using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class CompanyProjectPostRequest
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
    }
}
