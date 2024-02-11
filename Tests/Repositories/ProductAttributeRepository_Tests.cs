using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class ProductAttributeRepository_Tests
{

    private readonly DataContext2 _context =
new(new DbContextOptionsBuilder<DataContext2>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);

    [Fact]
    public void Create_ShouldCreateProductAttribute()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context);
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
        var productAttributeEntity = new ProductAttributeEntity { Id = 1, AttributeName = "Name test" };
        


        // Act
        var result = productAttributeRepository.Create(productAttributeEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllProductAttributes()
    {
        //Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context);
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
        var productAttributeEntity = new ProductAttributeEntity { Id = 1, AttributeName = "Name test" };
        productAttributeRepository.Create(productAttributeEntity);

        // Act
        var result = productAttributeRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductAttributeEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneProductAttribute()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context);
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
        var productAttributeEntity = new ProductAttributeEntity { Id = 1, AttributeName = "Name test" };
        productAttributeRepository.Create(productAttributeEntity);

        // Act
        var result = productAttributeRepository.Get(x => x.Id == productAttributeEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productAttributeEntity.Id, result.Id);


    }

    [Fact]
    public void Update_ShouldUpdateOneProductAttribute()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context);
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
        var productAttributeEntity = new ProductAttributeEntity { Id = 1, AttributeName = "Name test" };
        productAttributeRepository.Create(productAttributeEntity);

        // Act
        productAttributeEntity.AttributeName = "name test";
        productAttributeRepository.Update(x => x.Id == productAttributeEntity.Id, productAttributeEntity);
        var result = productAttributeRepository.Get(x => x.Id == productAttributeEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("name test", result.AttributeName);



    }

    [Fact]
    public void Delete_ShouldDeleteOneProductAttribute()
    {
        // Arrange
        var productAttributeRepository = new ProductAttributeRepository(_context);
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
        var productAttributeEntity = new ProductAttributeEntity { Id = 1, AttributeName = "Name test" };
        productAttributeRepository.Create(productAttributeEntity);

        // Act
        productAttributeRepository.Delete(x => x.Id == productAttributeEntity.Id);
        var result = productAttributeRepository.Get(x => x.Id == productAttributeEntity.Id);

        // Assert
        Assert.Null(result);



    }
}

