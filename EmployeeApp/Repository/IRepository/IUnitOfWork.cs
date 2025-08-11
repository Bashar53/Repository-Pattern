namespace EmployeeApp.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository employee { get; }
        Task SaveAsync();
    }
}
