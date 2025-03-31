namespace OrderManager.API.UnitsOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    Task RollbackAsync();
 }
