using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class EmployeePutRequest
    {
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
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        [Required]
        public int DepartmentID { get; set; }
    }
}
