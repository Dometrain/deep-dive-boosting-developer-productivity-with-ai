using OrderManager.API.Entities;
using OrderManager.API.Repositories;
using OrderManager.API.DbContexts;

namespace OrderManager.API.UnitsOfWork;

public class CreateOrderWithOrderLinesUnitOfWork : IUnitOfWork
{
    private readonly OrderManagerDbContext _context;
    private readonly IGenericRepository<Order> _orderRepository;
    private readonly IGenericRepository<OrderLine> _orderLineRepository; 

    public CreateOrderWithOrderLinesUnitOfWork(OrderManagerDbContext context,
        IGenericRepository<Order> orderRepository,
        IGenericRepository<OrderLine> orderLineRepository)
    {
        _context = context;
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        _context.ChangeTracker.Clear();
        await Task.CompletedTask;
    }

    public async Task CreateOrderWithOrderLinesAsync(Order order,
        IEnumerable<OrderLine> orderLines)
    {
        await _orderRepository.AddAsync(order);
        foreach (var line in orderLines)
        {
            line.Order = order;
            await _orderLineRepository.AddAsync(line);
        }
    }

    public void Dispose()
    {
        // nothing to see here
    }
}