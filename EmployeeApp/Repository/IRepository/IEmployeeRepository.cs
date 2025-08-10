using EmployeeApp.IRepository;
using EmployeeApp.Models;

namespace EmployeeApp.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> UpdateAsync(Employee employee);
    }
}
