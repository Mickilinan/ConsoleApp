using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class SupplierRepository_Tests
{

    private readonly DataContext2 _context =
     new(new DbContextOptionsBuilder<DataContext2>()
         .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);

    [Fact]
    public void Create_ShouldCreateSupplier()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierEntity = new SupplierEntity { Id = 1, ContactInfo = "Info test", SupplierName = "Name test" };


        // Act
        var result = supplierRepository.Create(supplierEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }
}
