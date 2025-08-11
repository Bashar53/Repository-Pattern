using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        
        public string Name { get; set; } 
      

        public string Department { get; set; }
       
        public decimal Salary { get; set; }
    }
}
