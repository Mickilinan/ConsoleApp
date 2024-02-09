using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class CategoryService_Tests
{
    private readonly DataContext _context =
       new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]  
    public void CreateCategory_ShouldCreateCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var category = new CategoryService(categoryRepository);
        string categoryName = "Test Category";

        // Act
        var result = category.CreateCategory(categoryName);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    [Fact]
    public void CreateCategory_ShouldReturnExistingCategory_WhenCategoryNameAlreadyExists()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Create a category
        var existingCategoryName = "Existing category";
        var existingCategory = categoryService.CreateCategory(existingCategoryName);

        // Act
        // Try to create another category with the same name
        var duplicateCategoryName = "Existing category";
        var result = categoryService.CreateCategory(duplicateCategoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCategory.Id, result.Id);
        Assert.Equal(existingCategory.CategoryName, result.CategoryName);
    }


    [Fact]
    public void GetCategoryByCategoryName_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var existingCategoryName = "Existing category";
        categoryService.CreateCategory(existingCategoryName);

        // Act
        var result = categoryService.GetCategoryByCategoryName(existingCategoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCategoryName, result.CategoryName);
    }

    [Fact]
    public void GetCategoryByCategoryName_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var result = categoryService.GetCategoryByCategoryName("Non-existing category");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateCategory_ShouldUpdateCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var existingCategory = categoryService.CreateCategory("Existing category");
        existingCategory.CategoryName = "Updated category";

        // Act
        var result = categoryService.UpdateCategory(existingCategory);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated category", result.CategoryName);
    }

    [Fact]
    public void UpdateCategory_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var nonExistingCategory = new CategoryEntity { Id = 999, CategoryName = "Non-existing category" }; // 999 is a non-existing Id

        // Act
        var result = categoryService.UpdateCategory(nonExistingCategory);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteCategory_ShouldDeleteCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);
        var existingCategory = categoryService.CreateCategory("Existing category");

        // Act
        categoryService.DeleteCategory(existingCategory.Id);
        var result = categoryService.GetCategoryByCategoryName("Existing category");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteCategory_ShouldNotThrowException_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepository);

        // Act
        var exception = Record.Exception(() => categoryService.DeleteCategory(999)); // 999 is a non-existing Id

        // Assert
        Assert.Null(exception);
    }
}
