using System.ComponentModel.DataAnnotations;

namespace SewPro.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
