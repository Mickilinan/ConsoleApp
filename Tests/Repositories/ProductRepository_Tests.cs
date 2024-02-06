using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class ProductRepository_Tests
{
    private readonly DataContext _context =
     new(new DbContextOptionsBuilder<DataContext>()
         .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);

    [Fact]
    public void Create_ShouldCreateProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productEntity = new ProductEntity { ProductName = "Test product" };


        // Act
        var result = productRepository.Create(productEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllProducts()
    {
        var productRepository = new ProductRepository(_context);
        var productEntity = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        productRepository.Create(productEntity);

        // Act
        var result = productRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productEntity = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };


        productRepository.Create(productEntity);

        // Act
        var result = productRepository.Get(x => x.Id == productEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productEntity.Id, result.Id);


    }

    [Fact]
    public void Update_ShouldUpdateOneProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productEntity = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        productRepository.Create(productEntity);

        // Act
        productEntity.Price = 200.0m; // Change a property
        productRepository.Update(x => x.Id == productEntity.Id, productEntity);
        var result = productRepository.Get(x => x.Id == productEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200.0m, result.Price);

    }


    [Fact]
    public void Delete_ShouldDeleteOneProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var productEntity = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        productRepository.Create(productEntity);

        // Act
        productRepository.Delete(x => x.Id == productEntity.Id);
        var result = productRepository.Get(x => x.Id == productEntity.Id);

        // Assert
        Assert.Null(result);



    }
}
