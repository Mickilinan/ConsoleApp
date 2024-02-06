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
        var productImageEntity = new ProductImageEntity { Id = 1, ImagePath = "Imagepath test" };


        // Act
        var result = productImageRepository.Create(productImageEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }
}
