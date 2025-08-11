using EmployeeApp.DbConnection;
using EmployeeApp.IRepository;
using EmployeeApp.Models;
using EmployeeApp.Repository.IRepository;

namespace EmployeeApp.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
           
            _applicationDbContext.Employee.Update(employee);
            await _applicationDbContext.SaveChangesAsync();
            return employee;
        }
    }
}
