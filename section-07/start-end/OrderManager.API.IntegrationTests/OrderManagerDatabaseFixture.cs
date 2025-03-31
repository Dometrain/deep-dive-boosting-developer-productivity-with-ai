using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.API.DbContexts;
using OrderManager.API.Repositories;
using OrderManager.API.Services;

namespace OrderManager.API.IntegrationTests;

public class OrderManagerDatabaseFixture : IDisposable
{
    private readonly SqliteConnection _connection;
    public IServiceProvider ServiceProvider { get; }

    public OrderManagerDatabaseFixture()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var services = new ServiceCollection();

        services.AddDbContext<OrderManagerDbContext>(options =>
            options.UseSqlite(_connection));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IDiscountService, DiscountService>();

        ServiceProvider = services.BuildServiceProvider();

        var context = ServiceProvider.GetRequiredService<OrderManagerDbContext>();
        context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Close();
    }

    public void ResetDatabase()
    {
        using var context = ServiceProvider.GetRequiredService<OrderManagerDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}
