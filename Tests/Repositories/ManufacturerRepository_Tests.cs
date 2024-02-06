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
        var manufacturerEntity = new ManufacturerEntity { Id = 1, ManufacturerName = "Name test" };


        // Act
        var result = manufacturerRepository.Create(manufacturerEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }
}
