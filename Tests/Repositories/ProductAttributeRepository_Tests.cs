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
        var productAttributeEntity = new ProductAttributeEntity { Id = 1, AttributeName = "Name test" };


        // Act
        var result = productAttributeRepository.Create(productAttributeEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }
}
