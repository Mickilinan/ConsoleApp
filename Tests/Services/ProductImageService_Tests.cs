using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class ProductImageService_Tests
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
    public void CreateProductImage_ShouldReturnProductImage_WhenProductImageIsCreated()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);

        string productName = "Test product";
        string imagePath = "Test path";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";



        // Act
        var result = productImageService.CreateProductImage(productName, imagePath, price, description, categoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(imagePath, result.ImagePath);
        Assert.NotNull(result.ProductId);
    }

    [Fact]
    public void CreateProductImage_ShouldReturnNull_WhenProductImageIdAlreadyExists()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);

        string productName = "Test product";
        string imagePath = "Test path";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        productImageService.CreateProductImage(productName, imagePath, price, description, categoryName);

        // Act
        var result = productImageService.CreateProductImage(productName, imagePath, price, description, categoryName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllProductImages_ShouldReturnAllProductImages()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);

        productImageService.CreateProductImage("Test product1", "Test path", 100, "Test description", "Test category");
        productImageService.CreateProductImage("Test product2", "Test path", 100, "Test description", "Test category");
        productImageService.CreateProductImage("Test product3", "Test path", 100, "Test description", "Test category");


        // Act
        var result = productImageService.GetAllProductImages();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetProductImageById_ShouldReturnProductImage_WhenProductImageExists()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);

        string productName = "Test product";
        string imagePath = "Test path";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdProductImage = productImageService.CreateProductImage(productName, imagePath, price, description, categoryName);

        // Act
        var result = productImageService.GetProductImageById(createdProductImage.Id);

        // Assert
        Assert.Equal(createdProductImage.Id, result.Id);
    }

    [Fact]
    public void GetProductImageById_ShouldReturnNull_WhenProductImageDoesNotExist()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);

        // Act
        var result = productImageService.GetProductImageById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateProductImage_ShouldReturnUpdatedProductImage_WhenProductImageExists()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);


        string productName = "Test product";
        string imagePath = "Test path";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdProductImage = productImageService.CreateProductImage(productName, imagePath, price, description, categoryName);

        createdProductImage.ImagePath = "Updated imagepath";

        // Act
        var result = productImageService.UpdateProductImage(createdProductImage);

        // Assert
        Assert.Equal("Updated imagepath", result.ImagePath);
    }

    [Fact]
    public void UpdateProductImage_ShouldReturnNull_WhenProductImageDoesNotExist()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);

        string productName = "Test product";
        string imagePath = "Test path";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";


        var nonExistentProductImage = new ProductImageEntity
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
            ImagePath = imagePath
        };

        // Act
        var result = productImageService.UpdateProductImage(nonExistentProductImage);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteProductImage_ShouldReturnTrue_WhenProductImageIsDeleted()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var productService = new ProductService(productRepository, categoryService);
        var productImageService = new ProductImageService(productImageRepository, productService);

        string productName = "Test product";
        string imagePath = "Test path";
        decimal price = 100.0m;
        string description = "Test description";
        string categoryName = "Test category";

        var createdProductImage = productImageService.CreateProductImage(productName, imagePath, price, description, categoryName);

        // Act
        productImageService.DeleteProductImage(createdProductImage.Id);

        // Assert
        var deletedProductImage = productImageService.GetProductImageById(createdProductImage.Id);
        Assert.Null(deletedProductImage);
    }
}
