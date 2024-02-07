using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class ProductImageRepository_Tests
{

    private readonly DataContext2 _context =
  new(new DbContextOptionsBuilder<DataContext2>()
      .UseInMemoryDatabase($"{Guid.NewGuid()}")
      .Options);

    [Fact]
    public void Create_ShouldCreateProductImage()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context);

        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };
        _context.Products.Add(product);
        _context.SaveChanges();
        var productImageEntity = new ProductImageEntity { Id = 1, ImagePath = "Imagepath test" };


        // Act
        var result = productImageRepository.Create(productImageEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllProductImages()
    {
        var productImageRepository = new ProductImageRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        _context.Products.Add(product);
        _context.SaveChanges();
        var productImageEntity = new ProductImageEntity { Id = 1, ImagePath = "Imagepath test" };
        productImageRepository.Create(productImageEntity);

        // Act
        var result = productImageRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductImageEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneProductImage()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };


        _context.Products.Add(product);
        _context.SaveChanges();
        var productImageEntity = new ProductImageEntity { Id = 1, ImagePath = "Imagepath test" };
        productImageRepository.Create(productImageEntity);

        // Act
        var result = productImageRepository.Get(x => x.Id == productImageEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productImageEntity.Id, result.Id);


    }

    [Fact]
    public void Update_ShouldUpdateOneProductImage()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };


        _context.Products.Add(product);
        _context.SaveChanges();
        var productImageEntity = new ProductImageEntity { Id = 1, ImagePath = "Imagepath test" };
        productImageRepository.Create(productImageEntity);

        // Act
        productImageEntity.ImagePath = "name test";
        productImageRepository.Update(x => x.Id == productImageEntity.Id, productImageEntity);
        var result = productImageRepository.Get(x => x.Id == productImageEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("name test", result.ImagePath);



    }


    [Fact]
    public void Delete_ShouldDeleteOneProductImage()
    {
        // Arrange
        var productImageRepository = new ProductImageRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };


        _context.Products.Add(product);
        _context.SaveChanges();
        var productImageEntity = new ProductImageEntity { Id = 1, ImagePath = "Imagepath test" };
        productImageRepository.Create(productImageEntity);

        // Act
        productImageRepository.Delete(x => x.Id == productImageEntity.Id);
        var result = productImageRepository.Get(x => x.Id == productImageEntity.Id);

        // Assert
        Assert.Null(result);



    }
}
