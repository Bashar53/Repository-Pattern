using EmployeeApp.Models;
using EmployeeApp.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.DbConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }
        public async Task addEmployee(Employee _msg)
        {
            var nameParam = new SqlParameter("@name", _msg.Name);
            var deptParam = new SqlParameter("@department", _msg.Department);
            var salaryParam = new SqlParameter("@salary", _msg.Salary);

            await  this.Database.ExecuteSqlRawAsync(
                "EXEC EmployeeInsert @name, @department, @salary",
                nameParam, deptParam, salaryParam
            );
        }

    }
}
