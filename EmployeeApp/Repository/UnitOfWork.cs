using EmployeeApp.DbConnection;
using EmployeeApp.Repository.IRepository;

namespace EmployeeApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IEmployeeRepository employee { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            employee = new EmployeeRepository(_db);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
