using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    [Index(nameof(EmailAddress), IsUnique = true)]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Skills { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }
        public ICollection<ParticipatingProject> ParticipatingProjects { get; set; }
    }
}
