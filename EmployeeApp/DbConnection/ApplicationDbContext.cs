using EmployeeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.DbConnection
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {

            }
            public DbSet<Employee> AppResources { get; set; }
           
        }
    }
