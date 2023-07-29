using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [PrimaryKey("CompanyProjectID", "EmployeeID")]
    public class ParticipatingProject
    {
        [Key]
        [Required]
        public int CompanyProjectID { get; set; }
        [Key]
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public ProjectRole ProjectRole { get; set; }

        [ForeignKey("CompanyProjectID")]
        public CompanyProject CompanyProject { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
