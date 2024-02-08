using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class ProductService_Tests
{
    
    private readonly DataContext _context =
       new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]
    public void CreateProduct_ShouldReturnProduct_WhenProductIsCreated()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        
        string productName = "Test Product";
        decimal price = 10;
        string description = "Test Description";
        string categoryName = "Test Category";

        // Act
        var result = productService.CreateProduct(productName, price, description, categoryName);

        // Assert
        Assert.Equal(productName, result.ProductName);
        Assert.Equal(price, result.Price);
        Assert.Equal(description, result.Description);
        Assert.NotNull(result.CategoryId);
    }

    [Fact]
    public void CreateProduct_ShouldReturnNull_WhenProductNameAlreadyExists()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);

        productService.CreateProduct("Test Product", 10m, "Test Description", "Test Category");

        // Act
        // Try to create another product with the same name
        var result = productService.CreateProduct("Test Product", 20m, "Another Description", "Another Category");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);

        productService.CreateProduct("Product 1", 10m, "Description 1", "Category 1");
        productService.CreateProduct("Product 2", 20m, "Description 2", "Category 2");
        productService.CreateProduct("Product 3", 30m, "Description 3", "Category 3");

        // Act
        var result = productService.GetAllProducts();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetProductById_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);

        string productName = "Test Product";
        decimal price = 10m;
        string description = "Test Description";
        string categoryName = "Test Category";

        var createdProduct = productService.CreateProduct(productName, price, description, categoryName);

        // Act
        var result = productService.GetProductById(createdProduct.Id);

        // Assert
        Assert.Equal(createdProduct.Id, result.Id);
    }

    [Fact]
    public void GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);

        // Act
        var result = productService.GetProductById(999); // Use an ID that does not exist

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateProduct_ShouldReturnUpdatedProduct_WhenProductExists()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);

        string productName = "Test Product";
        decimal price = 10m;
        string description = "Test Description";
        string categoryName = "Test Category";

        var createdProduct = productService.CreateProduct(productName, price, description, categoryName);

        createdProduct.ProductName = "Updated Product Name";

        // Act
        var result = productService.UpdateProduct(createdProduct);

        // Assert
        Assert.Equal("Updated Product Name", result.ProductName);
    }

    [Fact]
    public void UpdateProduct_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);

        var nonExistentProduct = new ProductEntity { Id = 999, ProductName = "Non-Existent Product", Price = 10m, Description = "Test Description", CategoryId = 1 }; // Use an ID that does not exist

        // Act
        var result = productService.UpdateProduct(nonExistentProduct);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteProduct_ShouldReturnTrue_WhenProductIsDeleted()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);

        string productName = "Test Product";
        decimal price = 10m;
        string description = "Test Description";
        string categoryName = "Test Category";

        var createdProduct = productService.CreateProduct(productName, price, description, categoryName);

        // Act
        var result = productService.DeleteProduct(createdProduct.Id);

        // Assert
        Assert.True(result);
    }

}

