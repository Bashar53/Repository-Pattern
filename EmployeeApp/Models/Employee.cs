using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public partial class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
