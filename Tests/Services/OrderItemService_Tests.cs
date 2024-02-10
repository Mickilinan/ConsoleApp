using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;
public class OrderItemService_Tests
{
    private readonly DataContext _context =
       new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

   

        private OrderItemService _orderItemService;
        private OrderService _orderService;
        private ProductService _productService;

        public OrderItemService_Tests()
        {
            var orderItemRepository = new OrderItemRepository(_context);
            var orderRepository = new OrderRepository(_context);
            var productRepository = new ProductRepository(_context);
            var userRepository = new UserRepository(_context);
            var categoryRepository = new CategoryRepository(_context);

            var userService = new UserService(userRepository);
            _orderService = new OrderService(orderRepository, userService);
            var categoryService = new CategoryService(categoryRepository);
            _productService = new ProductService(productRepository, categoryService);

            _orderItemService = new OrderItemService(orderItemRepository, _orderService, _productService);
        }

    [Fact]
    public void CreateOrderItem_ShouldReturnOrderItem_WhenOrderItemIsCreated()
    {
        // Arrange
        int quantity = 1;
        var order = _orderService.CreateOrder("status", DateTime.Now, DateTime.Now, "firstName", "lastName", "email@test.com");
        var product = _productService.CreateProduct("productName", 100m, "description", "categoryName");

        // Act
        var result = _orderItemService.CreateOrderItem(quantity, order.Id, product.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(quantity, result.Quantity);
        Assert.Equal(order.Id, result.OrderId);
        Assert.Equal(product.Id, result.ProductId);
    }

    [Fact]
    public void GetOrderItemById_ShouldReturnOrderItem_WhenOrderItemExists()
    {
        // Arrange
        int quantity = 1;
        var order = _orderService.CreateOrder("status", DateTime.Now, DateTime.Now, "firstName", "lastName", "email@test.com");
        var product = _productService.CreateProduct("productName", 100m, "description", "categoryName");
        var createdOrderItem = _orderItemService.CreateOrderItem(quantity, order.Id, product.Id);

        // Act
        var result = _orderItemService.GetOrderItemById(createdOrderItem.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createdOrderItem.Id, result.Id);
    }

    [Fact]
    public void CreateOrderItem_ShouldReturnNull_WhenOrderIdDoesNotExist()
    {
        // Arrange
        int quantity = 1;
        int orderId = 999; // This should be an ID of a non-existing order
        int productId = 1; // This should be an ID of an existing product

        // Act
        var result = _orderItemService.CreateOrderItem(quantity, orderId, productId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetOrderItemById_ShouldReturnNull_WhenOrderItemIdDoesNotExist()
    {
        // Arrange
        int id = 999; // This should be an ID of a non-existing order item

        // Act
        var result = _orderItemService.GetOrderItemById(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllOrderItemsByOrderId_ShouldReturnOrderItems_WhenOrderItemsExist()
    {
        // Arrange
        var order = _orderService.CreateOrder("status", DateTime.Now, DateTime.Now, "firstName", "lastName", "email@test.com");
        var product = _productService.CreateProduct("productName", 100m, "description", "categoryName");
        _orderItemService.CreateOrderItem(1, order.Id, product.Id);

        // Act
        var result = _orderItemService.GetOrderItemsByOrderId(order.Id);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GetAllOrderItemsByOrderId_ShouldReturnEmpty_WhenOrderIdDoesNotExist()
    {
        // Arrange
        int orderId = 999; // This should be an ID of a non-existing order

        // Act
        var result = _orderItemService.GetOrderItemsByOrderId(orderId);

        // Assert
        Assert.Empty(result);
    }
    [Fact]
    public void GetAllOrderItems_ShouldReturnAllOrderItems()
    {
        // Arrange
        int quantity = 1;
        var order = _orderService.CreateOrder("status", DateTime.Now, DateTime.Now, "firstName", "lastName", "email@test.com");
        var product = _productService.CreateProduct("productName", 100m, "description", "categoryName");
        _orderItemService.CreateOrderItem(quantity, order.Id, product.Id);

        // Act
        var result = _orderItemService.GetAllOrderItems();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void UpdateOrderItem_ShouldReturnUpdatedOrderItem_WhenOrderItemExists()
    {
        // Arrange
        int quantity = 1;
        var order = _orderService.CreateOrder("status", DateTime.Now, DateTime.Now, "firstName", "lastName", "email@test.com");
        var product = _productService.CreateProduct("productName", 100m, "description", "categoryName");
        var createdOrderItem = _orderItemService.CreateOrderItem(quantity, order.Id, product.Id);
        createdOrderItem.Quantity = 2;

        // Act
        var result = _orderItemService.UpdateOrderItem(createdOrderItem);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Quantity);
    }
    [Fact]
    public void UpdateOrderItem_ShouldReturnNull_WhenOrderItemIdDoesNotExist()
    {
        // Arrange
        var orderItem = new OrderItemEntity
        {
            Id = 999, // This should be an ID of a non-existing order item
            Quantity = 2,
            OrderId = 1, // This should be an ID of an existing order
            ProductId = 1 // This should be an ID of an existing product
        };

        // Act
        var result = _orderItemService.UpdateOrderItem(orderItem);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteOrderItem_ShouldNotThrowException_WhenOrderItemExists()
    {
        // Arrange
        int quantity = 1;
        var order = _orderService.CreateOrder("status", DateTime.Now, DateTime.Now, "firstName", "lastName", "email@test.com");
        var product = _productService.CreateProduct("productName", 100m, "description", "categoryName");
        var createdOrderItem = _orderItemService.CreateOrderItem(quantity, order.Id, product.Id);

      
        // Act
        _orderItemService.DeleteOrderItem(createdOrderItem.Id);

        // Assert
        var deletedOrderItem = _orderItemService.GetOrderItemById(createdOrderItem.Id);
        Assert.Null(deletedOrderItem);
    }
    [Fact]
    public void DeleteOrderItem_ShouldNotThrowException_WhenOrderItemIdDoesNotExist()
    {
        // Arrange
        int id = 999; // This should be an ID of a non-existing order item

        // Act
        Exception ex = Record.Exception(() => _orderItemService.DeleteOrderItem(id));

        // Assert
        Assert.Null(ex);
    }

}


