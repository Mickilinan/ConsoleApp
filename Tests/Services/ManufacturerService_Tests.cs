using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class ManufacturerService_Tests
{

    private readonly DataContext2 _context2 =
     new(new DbContextOptionsBuilder<DataContext2>()
         .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);

    private readonly DataContext _context =
      new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);



    [Fact]
    public void CreateManufacturer_ShouldReturnManufacturer_WhenManufacturerIsCreated()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        string productName = "Test product";
        string manufacturerName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";



        // Act
        var result = manufacturerService.CreateManufacturer(productName, manufacturerName, price, description,categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(manufacturerName, result.ManufacturerName);
        Assert.NotNull(result.ProductId);
    }

    [Fact]
    public void CreateManufacturer_ShouldReturnNull_WhenManufacturerIdAlreadyExists()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        string productName = "Test product";
        string manufacturerName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        manufacturerService.CreateManufacturer(productName, manufacturerName, price, description, categoryName);

        // Act
        var result = manufacturerService.CreateManufacturer(productName, manufacturerName, price, description, categoryName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllManufacturers_ShouldReturnAllManufacturers()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        manufacturerService.CreateManufacturer("Test product1", "Test name", 100, "Test description", "Test category");
        manufacturerService.CreateManufacturer("Test product2", "Test name", 100, "Test description", "Test category");
        manufacturerService.CreateManufacturer("Test product3", "Test name", 100, "Test description", "Test category");


        // Act
        var result = manufacturerService.GetAllManufacturers();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetManufacturerById_ShouldReturnManufacturer_WhenManufacturerExists()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        string productName = "Test product";
        string manufacturerName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdManufacturer = manufacturerService.CreateManufacturer(productName, manufacturerName, price, description, categoryName);

        // Act
        var result = manufacturerService.GetManufacturerById(createdManufacturer.Id);

        // Assert
        Assert.Equal(createdManufacturer.Id, result.Id);
    }

    [Fact]
    public void GetManufacturerById_ShouldReturnNull_WhenManufacturerDoesNotExist()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        // Act
        var result = manufacturerService.GetManufacturerById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateManufacturer_ShouldReturnUpdatedManufacturer_WhenManufacturerExists()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        string productName = "Test product";
        string manufacturerName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdManufacturer = manufacturerService.CreateManufacturer(productName, manufacturerName, price, description, categoryName);

        createdManufacturer.ManufacturerName = "Updated manufacturer name";

        // Act
        var result = manufacturerService.UpdateManufacturer(createdManufacturer);

        // Assert
        Assert.Equal("Updated manufacturer name", result.ManufacturerName);
    }

    [Fact]
    public void UpdateManufacturer_ShouldReturnNull_WhenManufacturerDoesNotExist()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        string productName = "Test product";
        string manufacturerName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";



        var nonExistentManufacturer = new ManufacturerEntity
        {
            Product = new ProductEntity
            {
                ProductName = productName,
                Price = price,
                Description = description,
                Category = new CategoryEntity
                {
                    CategoryName = categoryName
                }
            },
            ManufacturerName = manufacturerName
        };

        // Act
        var result = manufacturerService.UpdateManufacturer(nonExistentManufacturer);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteManufacturer_ShouldReturnTrue_WhenManufacturerIsDeleted()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var manufacturerService = new ManufacturerService(manufacturerRepository, productService);

        string productName = "Test product";
        string manufacturerName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdManufacturer = manufacturerService.CreateManufacturer(productName, manufacturerName, price, description, categoryName);

        // Act
        manufacturerService.DeleteManufacturer(createdManufacturer.Id);

        // Assert
        var deletedManufacturer = manufacturerService.GetManufacturerById(createdManufacturer.Id);
        Assert.Null(deletedManufacturer);
    }
}
