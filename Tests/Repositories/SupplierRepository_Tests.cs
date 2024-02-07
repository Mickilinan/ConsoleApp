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


    [Fact]
    public void Get_ShouldGetAllSuppliers()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierEntity = new SupplierEntity { Id = 1, ContactInfo = "Info test", SupplierName = "Name test" };
        supplierRepository.Create(supplierEntity);


        // Act
        var result = supplierRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<SupplierEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneSupplier()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierEntity = new SupplierEntity { Id = 1, ContactInfo = "Info test", SupplierName = "Name test" };
        supplierRepository.Create(supplierEntity);


        // Act
        var result = supplierRepository.Get(x => x.SupplierName == supplierEntity.SupplierName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(supplierEntity.SupplierName, result.SupplierName);


    }

    [Fact]
    public void Update_ShouldUpdateOneSupplier()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierEntity = new SupplierEntity { Id = 1, ContactInfo = "Info test", SupplierName = "Name test" };
        supplierEntity = supplierRepository.Create(supplierEntity);

        // Act
        supplierEntity.SupplierName = "Updated supplier";
        var result = supplierRepository.Update(x => x.Id == supplierEntity.Id, supplierEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(supplierEntity.Id, result.Id);
        Assert.Equal(supplierEntity.SupplierName, result.SupplierName);



    }


    [Fact]
    public void Delete_ShouldDeleteOneSupplier()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierEntity = new SupplierEntity { Id = 1, ContactInfo = "Info test", SupplierName = "Name test" };
        supplierRepository.Create(supplierEntity);

        // Act
        var result = supplierRepository.Delete(x => x.SupplierName == supplierEntity.SupplierName);

        // Assert
        Assert.True(result);



    }

}
