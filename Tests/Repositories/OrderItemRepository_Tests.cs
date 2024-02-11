using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class OrderItemRepository_Tests
{
    private readonly DataContext _context =
   new(new DbContextOptionsBuilder<DataContext>()
       .UseInMemoryDatabase($"{Guid.NewGuid()}")
       .Options);

    [Fact]
    public void Create_ShouldCreateOrderItem()
    {
        // Arrange
        var orderItemRepository = new OrderItemRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };
        var order = new OrderEntity {
            UserId = 1,
            Status = "New",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _context.Products.Add(product);
        _context.Orders.Add(order);
        _context.SaveChanges();
        var orderItemEntity = new OrderItemEntity { Id = 1, Product = product, Order = order };


        // Act
        var result = orderItemRepository.Create(orderItemEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllOrderItems()
    {
        //Arrange
        var orderItemRepository = new OrderItemRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };
        var order = new OrderEntity
        {
            UserId = 1,
            Status = "New",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _context.Products.Add(product);
        _context.Orders.Add(order);
        _context.SaveChanges();
        var orderItemEntity = new OrderItemEntity { Quantity = 1, Product = product, Order = order };
        orderItemRepository.Create(orderItemEntity);

        // Act
        var result = orderItemRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<OrderItemEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneOrderItem()
    {
        // Arrange
        var orderItemRepository = new OrderItemRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        var order = new OrderEntity {
            UserId = 1,
            Status = "New",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _context.Products.Add(product);
        _context.Orders.Add(order);
        _context.SaveChanges();
        var orderItemEntity = new OrderItemEntity { Id = 1, Product = product, Order = order };
        orderItemRepository.Create(orderItemEntity);

        // Act
        var result = orderItemRepository.Get(x => x.Id == orderItemEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderItemEntity.Id, result.Id);


    }

    [Fact]
    public void Update_ShouldUpdateOneOrderItem()
    {
        // Arrange
        var orderItemRepository = new OrderItemRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        var order = new OrderEntity {
            UserId = 1,
            Status = "New",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _context.Products.Add(product);
        _context.Orders.Add(order);
        _context.SaveChanges();
        var orderItemEntity = new OrderItemEntity { Id = 1, Product = product, Order = order };
        orderItemRepository.Create(orderItemEntity);

        // Act
        orderItemEntity.Quantity = 2; 
        orderItemRepository.Update(x => x.Id == orderItemEntity.Id, orderItemEntity);
        var result = orderItemRepository.Get(x => x.Id == orderItemEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Quantity);



    }

    [Fact]
    public void Delete_ShouldDeleteOneOrderItem()
    {
        // Arrange
        var orderItemRepository = new OrderItemRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        var order = new OrderEntity { 
            UserId = 1,
            Status = "New",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        _context.Products.Add(product);
        _context.Orders.Add(order);
        _context.SaveChanges();
        var orderItemEntity = new OrderItemEntity { Id = 1, Product = product, Order = order };
        orderItemRepository.Create(orderItemEntity);

        // Act
        orderItemRepository.Delete(x => x.Id == orderItemEntity.Id);
        var result = orderItemRepository.Get(x => x.Id == orderItemEntity.Id);

        // Assert
        Assert.Null(result);



    }

}
