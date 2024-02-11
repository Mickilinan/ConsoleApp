using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class CategoryRepository_Tests
{

    private readonly DataContext _context =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void Create_ShouldCreateCategory()
    {
        // Arrange
       var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };

           
            // Act
         var result = categoryRepository.Create(categoryEntity);   

            // Assert
         Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        
    }

    [Fact]
    public void Get_ShouldGetAllCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
        categoryRepository.Create(categoryEntity);
        

        // Act
        var result = categoryRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CategoryEntity>>(result);
        Assert.Single(result);
        

    }

    [Fact]
    public void Get_ShouldGetOneCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
        categoryRepository.Create(categoryEntity);


        // Act
        var result = categoryRepository.Get(x => x.CategoryName == categoryEntity.CategoryName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.CategoryName, result.CategoryName);


    }

    [Fact]
    public void Update_ShouldUpdateOneCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
        categoryEntity = categoryRepository.Create(categoryEntity);

        // Act
        categoryEntity.CategoryName = "Updated Category";
        var result = categoryRepository.Update(x => x.Id == categoryEntity.Id, categoryEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.Id, result.Id);
        Assert.Equal(categoryEntity.CategoryName, result.CategoryName);



    }

    [Fact]
    public void Delete_ShouldDeleteOneCategory()
    {
        // Arrange
        var categoryRepository = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
        categoryRepository.Create(categoryEntity);
        
        // Act
        var result = categoryRepository.Delete(x => x.CategoryName == categoryEntity.CategoryName);

        // Assert
        Assert.True(result);
        


    }


}
