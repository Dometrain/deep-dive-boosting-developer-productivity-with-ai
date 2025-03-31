using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.API.DbContexts;
using OrderManager.API.Entities;
using OrderManager.API.Repositories;
using OrderManager.API.Services;
using OrderManager.API.UnitsOfWork;
using System;

namespace OrderManager.API.EndToEndTests;

public class OrdersTestsFixture : IDisposable
{
    public WebApplicationFactory<Program> Factory { get; }

    public OrdersTestsFixture()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing DbContext registration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<OrderManagerDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add SQLite in-memory database
                    services.AddDbContext<OrderManagerDbContext>(options =>
                    {
                        options.UseSqlite("DataSource=:memory:");
                    });

                    // Register dependencies
                    services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
                    services.AddScoped<IGenericRepository<OrderLine>, GenericRepository<OrderLine>>();
                    services.AddScoped<IOrderRepository, OrderRepository>();
                    services.AddScoped<IDiscountService, DiscountService>();
                    services.AddScoped<CreateOrderWithOrderLinesUnitOfWork>();

                    // Build the service provider
                    var serviceProvider = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database context
                    using var scope = serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<OrderManagerDbContext>();

                    // Ensure the database is created
                    dbContext.Database.OpenConnection();
                    dbContext.Database.EnsureCreated();  

                    // Seed the database with test data
                    SeedDatabase(dbContext);
                });
            });
    }

    private void SeedDatabase(OrderManagerDbContext dbContext)
    {  
        var orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                Title = "Test Order 1",
                Description = "Description for Test Order 1",
                OrderDate = DateTime.UtcNow,
                ShippingDate = DateTime.UtcNow.AddDays(1),
                OrderTotal = 100.00m,
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Id = 1,
                        Details = "Order Line 1",
                        Amount = 1,
                        Price = 50.00m,
                        ProductId = 1,
                        OrderId = 1
                    },
                    new OrderLine
                    {
                        Id = 2,
                        Details = "Order Line 2",
                        Amount = 1,
                        Price = 50.00m,
                        ProductId = 2,
                        OrderId = 1
                    }
                }
            }
        };

        dbContext.Orders.AddRange(orders);
        dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Factory.Dispose();
    }
}
