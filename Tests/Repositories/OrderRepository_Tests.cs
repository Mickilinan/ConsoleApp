using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class OrderRepository_Tests
{

    private readonly DataContext _context =
       new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]
    public void Create_ShouldCreateOrder()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var orderEntity = new OrderEntity { Status = "Status test"};


        // Act
        var result = orderRepository.Create(orderEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }
}
