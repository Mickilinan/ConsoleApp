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
        var userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com"
        };
        _context.Users.Add(userEntity);
        _context.SaveChanges();
        var orderEntity = new OrderEntity { Status = "Status test", User = userEntity };

        // Act
        var result = orderRepository.Create(orderEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllOrders()
    {
        var orderRepository = new OrderRepository(_context);
        var userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com" 
        };

        _context.Users.Add(userEntity);
        _context.SaveChanges();
        var orderEntity = new OrderEntity { Status = "Status test", User = userEntity };
        orderRepository.Create(orderEntity);

        // Act
        var result = orderRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<OrderEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneOrder()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com" 
        };
        _context.Users.Add(userEntity);
        _context.SaveChanges();
        var orderEntity = new OrderEntity { Status = "Status test", User = userEntity };
        orderRepository.Create(orderEntity);

        

        // Act
        var result = orderRepository.Get(x => x.Id == orderEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderEntity.Id, result.Id);


    }

    [Fact]
    public void Update_ShouldUpdateOneOrder()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com"
        };

        _context.Users.Add(userEntity);
        _context.SaveChanges();
        var orderEntity = new OrderEntity { Status = "Status test", User = userEntity };
        orderRepository.Create(orderEntity);
        

        // Act
        orderEntity.Status = "Updated status"; // Change a property
        orderRepository.Update(x => x.Id == orderEntity.Id, orderEntity);
        var result = orderRepository.Get(x => x.Id == orderEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated status", result.Status);

    }


    [Fact]
    public void Delete_ShouldDeleteOneOrder()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userEntity = new UserEntity
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com"
        };

        _context.Users.Add(userEntity);
        _context.SaveChanges();
        var orderEntity = new OrderEntity { Status = "Status test", User = userEntity };
        orderRepository.Create(orderEntity);

        // Act
        orderRepository.Delete(x => x.Id == orderEntity.Id);
        var result = orderRepository.Get(x => x.Id == orderEntity.Id);

        // Assert
        Assert.Null(result);



    }
}
