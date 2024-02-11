using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class SupplierService_Tests
{

    private readonly DataContext2 _context =
     new(new DbContextOptionsBuilder<DataContext2>()
         .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);

    [Fact]
    public void CreateSupplier_ShouldCreateSupplier()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplier = new SupplierService(supplierRepository);
        string supplierName = "Test supplier";
       string contactInfo = "Test contact info";

        // Act
        var result = supplier.CreateSupplier(supplierName, contactInfo);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    [Fact]
    public void CreateSupplier_ShouldReturnNull_WhenSupplierNameAlreadyExists()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierService = new SupplierService(supplierRepository);

        
        var existingSupplierName = "Existing supplier";
        supplierService.CreateSupplier(existingSupplierName, "Existing contact info");

        // Act
        var duplicateSupplierName = "Existing supplier";
        var result = supplierService.CreateSupplier(duplicateSupplierName, "Duplicate contact info");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetSupplierBySupplierName_ShouldReturnSupplier_WhenSupplierExists()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierService = new SupplierService(supplierRepository);
        var existingSupplierName = "Existing supplier";
        supplierService.CreateSupplier(existingSupplierName, "Existing contact info");

        // Act
        var result = supplierService.GetSupplierById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingSupplierName, result.SupplierName);
    }

    [Fact]
    public void GetSupplierBySupplierName_ShouldReturnNull_WhenSupplierDoesNotExist()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierService = new SupplierService(supplierRepository);

        // Act
        var result = supplierService.GetSupplierById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateSupplier_ShouldUpdateSupplier_WhenSupplierExists()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierService = new SupplierService(supplierRepository);
        var existingSupplier = supplierService.CreateSupplier("Existing supplier", "Existing contact info");
        existingSupplier.SupplierName = "Updated supplier";

        // Act
        var result = supplierService.UpdateSupplier(existingSupplier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated supplier", result.SupplierName);
    }

    [Fact]
    public void UpdateSupplier_ShouldReturnNull_WhenSupplierDoesNotExist()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierService = new SupplierService(supplierRepository);
        var nonExistingSupplier = new SupplierEntity { Id = 999, SupplierName = "Non-existing supplier" }; 

        // Act
        var result = supplierService.UpdateSupplier(nonExistingSupplier);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteSupplier_ShouldDeleteSupplier_WhenSupplierExists()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierService = new SupplierService(supplierRepository);
        var existingSupplier = supplierService.CreateSupplier("Existing supplier", "Existing contact info");

        // Act
        supplierService.DeleteSupplier(existingSupplier.Id);
        var result = supplierService.GetSupplierById(existingSupplier.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteSupplier_ShouldNotThrowException_WhenSupplierDoesNotExist()
    {
        // Arrange
        var supplierRepository = new SupplierRepository(_context);
        var supplierService = new SupplierService(supplierRepository);

        // Act
        var exception = Record.Exception(() => supplierService.DeleteSupplier(999)); 

        // Assert
        Assert.Null(exception);
    }
}
