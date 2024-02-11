using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;


namespace Tests.Services;

public class ProductAttributeService_Tests
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
    public void CreateProductAttribute_ShouldReturnProductAttribute_WhenProductAttributeIsCreated()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

        string productName = "Test product";
        string attributeName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";



        // Act
        var result = productAttributeService.CreateProductAttribute(productName, attributeName, price, description, categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(attributeName, result.AttributeName);
        Assert.NotNull(result.ProductId);
    }

    [Fact]
    public void CreateProductAttribute_ShouldReturnNull_WhenProductAttributeIdAlreadyExists()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

        string productName = "Test product";
        string attributeName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        productAttributeService.CreateProductAttribute(productName, attributeName, price, description, categoryName);

        // Act
        var result = productAttributeService.CreateProductAttribute(productName, attributeName, price, description, categoryName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllProductAttributes_ShouldReturnAllProductAttributes()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

        productAttributeService.CreateProductAttribute("Test product1", "Test name", 100, "Test description", "Test category");
        productAttributeService.CreateProductAttribute("Test product2", "Test name", 100, "Test description", "Test category");
        productAttributeService.CreateProductAttribute("Test product3", "Test name", 100, "Test description", "Test category");


        // Act
        var result = productAttributeService.GetAllProductAttributes();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetProductAttributeById_ShouldReturnProductAttribute_WhenProductAttributeExists()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

        string productName = "Test product";
        string attributeName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdProductAttribute = productAttributeService.CreateProductAttribute(productName, attributeName, price, description, categoryName);

        // Act
        var result = productAttributeService.GetProductAttributeById(createdProductAttribute.Id);

        // Assert
        Assert.Equal(createdProductAttribute.Id, result.Id);
    }

    [Fact]
    public void GetProductAttributeById_ShouldReturnNull_WhenProductAttributeDoesNotExist()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

        // Act
        var result = productAttributeService.GetProductAttributeById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateProductAttribute_ShouldReturnUpdatedProductAttribute_WhenProductAttributeExists()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

       
        string productName = "Test product";
        string attributeName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdProductAttribute = productAttributeService.CreateProductAttribute(productName, attributeName, price, description, categoryName);

        createdProductAttribute.AttributeName = "Updated attribute name";

        // Act
        var result = productAttributeService.UpdateProductAttribute(createdProductAttribute);

        // Assert
        Assert.Equal("Updated attribute name", result.AttributeName);
    }

    [Fact]
    public void UpdateProductAttribute_ShouldReturnNull_WhenProductAttributeDoesNotExist()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

        string productName = "Test product";
        string attributeName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";


        var nonExistentProductAttribute = new ProductAttributeEntity
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
            AttributeName = attributeName
        };

        // Act
        var result = productAttributeService.UpdateProductAttribute(nonExistentProductAttribute);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteProductAttribute_ShouldReturnTrue_WhenProductAttributeIsDeleted()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productAttributeService = new ProductAttributeService(productAttributeRepository, productService);

        string productName = "Test product";
        string attributeName = "Test name";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdProductAttribute = productAttributeService.CreateProductAttribute(productName, attributeName, price, description, categoryName);

        // Act
        productAttributeService.DeleteProductAttribute(createdProductAttribute.Id);

        // Assert
        var deletedProductAttribute = productAttributeService.GetProductAttributeById(createdProductAttribute.Id);
        Assert.Null(deletedProductAttribute);
    }
}
