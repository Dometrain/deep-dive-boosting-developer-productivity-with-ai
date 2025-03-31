using Microsoft.EntityFrameworkCore;
using OrderManager.API.DbContexts;

namespace OrderManager.API.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly OrderManagerDbContext _context;

    public GenericRepository(OrderManagerDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        // Removed SaveChangesAsync call
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        // Removed SaveChangesAsync call
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            // Removed SaveChangesAsync call
        }
    }
}

