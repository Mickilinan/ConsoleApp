using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class ManufacturerRepository_Tests
{
    private readonly DataContext2 _context =
     new(new DbContextOptionsBuilder<DataContext2>()
         .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);

    [Fact]
    public void Create_ShouldCreateManufacturer()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);
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
        var manufacturerEntity = new ManufacturerEntity { Id = 1, ManufacturerName = "Name test" };


        // Act
        var result = manufacturerRepository.Create(manufacturerEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllManufacturers()
    {
        //Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);
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
        var manufacturerEntity = new ManufacturerEntity { Id = 1, ManufacturerName = "Name test" };
        manufacturerRepository.Create(manufacturerEntity);

        // Act
        var result = manufacturerRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ManufacturerEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneManufacturer()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);
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
        var manufacturerEntity = new ManufacturerEntity { Id = 1, ManufacturerName = "Name test" };
        manufacturerRepository.Create(manufacturerEntity);

        // Act
        var result = manufacturerRepository.Get(x => x.Id == manufacturerEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(manufacturerEntity.Id, result.Id);


    }

    [Fact]
    public void Update_ShouldUpdateOneManufacturer()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);
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
        var manufacturerEntity = new ManufacturerEntity { Id = 1, ManufacturerName = "Name test" };
        manufacturerRepository.Create(manufacturerEntity);

        // Act
        manufacturerEntity.ManufacturerName = "name test";
        manufacturerRepository.Update(x => x.Id == manufacturerEntity.Id, manufacturerEntity);
        var result = manufacturerRepository.Get(x => x.Id == manufacturerEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("name test",result.ManufacturerName);



    }

    [Fact]
    public void Delete_ShouldDeleteOneManufacturer()
    {
        // Arrange
        var manufacturerRepository = new ManufacturerRepository(_context);
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
        var manufacturerEntity = new ManufacturerEntity { Id = 1, ManufacturerName = "Name test" };
        manufacturerRepository.Create(manufacturerEntity);

        // Act
        manufacturerRepository.Delete(x => x.Id == manufacturerEntity.Id);
        var result = manufacturerRepository.Get(x => x.Id == manufacturerEntity.Id);

        // Assert
        Assert.Null(result);



    }
}
