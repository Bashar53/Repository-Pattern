using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]

        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
