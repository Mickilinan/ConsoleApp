using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class OrderService_Tests
{

    private readonly DataContext _context =
       new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]
    public void CreateOrder_ShouldReturnOrder_WhenOrderIsCreated()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        string status = "Test status";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;
        string firstName = "Test user";
        string lastName = "Test user";
        string email = "Test email";


        // Act
        var result = orderService.CreateOrder(status, createdAt, updatedAt, firstName, lastName, email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(status, result.Status);
        Assert.Equal(createdAt, result.CreatedAt);
        Assert.Equal(updatedAt, result.UpdatedAt);
        Assert.NotNull(result.UserId);
    }

    [Fact]
    public void UserCanHaveMultipleOrders()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        string status1 = "Test status 1";
        string status2 = "Test status 2";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;
        string firstName = "Test user";
        string lastName = "Test user";
        string email = "Test email";

        // Act
        var order1 = orderService.CreateOrder(status1, createdAt, updatedAt, firstName, lastName, email);
        var order2 = orderService.CreateOrder(status2, createdAt, updatedAt, firstName, lastName, email);

        // Assert
        Assert.NotNull(order1);
        Assert.NotNull(order2);
        Assert.Equal(firstName, order1.User.FirstName);
        Assert.Equal(firstName, order2.User.FirstName);
        Assert.Equal(status1, order1.Status);
        Assert.Equal(status2, order2.Status);
    }

    [Fact]
    public void GetAllOrders_ShouldReturnAllOrders()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        orderService.CreateOrder("Status 1", DateTime.Now, DateTime.Now, "User 1", "User 1", "User1@email.com");
        orderService.CreateOrder("Status 2", DateTime.Now, DateTime.Now, "User 2", "User 2", "User2@email.com");
        orderService.CreateOrder("Status 3", DateTime.Now, DateTime.Now, "User 3", "User 3", "User3@email.com");


        // Act
        var result = orderService.GetAllOrders();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetOrderById_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        string status = "Test status";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;
        string firstName = "Test user";
        string lastName = "Test user";
        string email = "Test email";

        var createdOrder = orderService.CreateOrder(status, createdAt, updatedAt, firstName, lastName, email);

        // Act
        var result = orderService.GetOrderById(createdOrder.Id);

        // Assert
        Assert.Equal(createdOrder.Id, result.Id);
    }

    [Fact]
    public void GetOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        // Act
        var result = orderService.GetOrderById(999); 

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateOrder_ShouldReturnUpdatedOrder_WhenOrderExists()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        string status = "Test status";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;
        string firstName = "Test user";
        string lastName = "Test user";
        string email = "Test email";

        var createdOrder = orderService.CreateOrder(status, createdAt, updatedAt, firstName, lastName, email);

        createdOrder.Status = "Updated order status";

        // Act
        var result = orderService.UpdateOrder(createdOrder);

        // Assert
        Assert.Equal("Updated order status", result.Status);
    }

    [Fact]
    public void UpdateOrder_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        string status = "Test status";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;
        string firstName = "Test user";
        string lastName = "Test user";
        string email = "Test email";



        var nonExistentOrder = new OrderEntity
        {
            Status = status,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
            User = new UserEntity

            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            }
        }; 

        // Act
        var result = orderService.UpdateOrder(nonExistentOrder);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteOrder_ShouldReturnTrue_WhenOrderIsDeleted()
    {
        // Arrange
        var orderRepository = new OrderRepository(_context);
        var userRepository = new UserRepository(_context);
        var userService = new UserService(userRepository);
        var orderService = new OrderService(orderRepository, userService);

        string status = "Test status";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;
        string firstName = "Test user";
        string lastName = "Test user";
        string email = "Test email";

        var createdOrder = orderService.CreateOrder(status, createdAt, updatedAt, firstName, lastName, email);

        // Act
        orderService.DeleteOrder(createdOrder.Id);

        // Assert
        var deletedOrder = orderService.GetOrderById(createdOrder.Id);
        Assert.Null(deletedOrder);
    }
}
