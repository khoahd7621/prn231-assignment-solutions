using BusinessObjects.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class ParticipantPostRequest
    {
        [Required]
        public int CompanyProjectID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public ProjectRole ProjectRole { get; set; }
    }
}
