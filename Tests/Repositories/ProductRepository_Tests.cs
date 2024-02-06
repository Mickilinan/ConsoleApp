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
}
