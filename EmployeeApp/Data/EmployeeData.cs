using EmployeeApp.Models.DTO;

namespace EmployeeApp.Data
{
    public static class EmployeeData
    {
        public static List<EmployeeDTO> EmployeeList = new List<EmployeeDTO>
        {
            new EmployeeDTO{ EmployeeId =  1,Name = "Hasan", Department="ICT",Salary=1000000 },  
            new EmployeeDTO{ EmployeeId =  2,Name = "Rakib", Department="ICT",Salary=5000000 },  
        };
    }
}
