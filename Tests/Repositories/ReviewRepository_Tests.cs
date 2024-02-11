using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class ReviewRepository_Tests
{

    private readonly DataContext2 _context =
    new(new DbContextOptionsBuilder<DataContext2>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public void Create_ShouldCreateReview()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context);
         var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };
        var user = new UserEntity {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test"
        };
        _context.Products.Add(product);
        _context.Users.Add(user);
        _context.SaveChanges();
        var reviewEntity = new ReviewEntity { Id = 1, Comment = "test", Product = product, User = user };


        // Act
        var result = reviewRepository.Create(reviewEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_ShouldGetAllReviews()
    {
        //Arrange
        var reviewRepository = new ReviewRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };
        var user = new UserEntity
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test"
        };
        _context.Products.Add(product);
        _context.Users.Add(user);
        _context.SaveChanges();
        var reviewEntity = new ReviewEntity { Id = 1, Comment = "test", Product = product, User = user };
        reviewRepository.Create(reviewEntity);

        // Act
        var result = reviewRepository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ReviewEntity>>(result);
        Assert.Single(result);


    }

    [Fact]
    public void Get_ShouldGetOneReview()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        var user = new UserEntity
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test"
        };
        _context.Products.Add(product);
        _context.Users.Add(user);
        _context.SaveChanges();
        var reviewEntity = new ReviewEntity { Id = 1, Comment = "test", Product = product, User = user };
        reviewRepository.Create(reviewEntity);

        // Act
        var result = reviewRepository.Get(x => x.Id == reviewEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reviewEntity.Id, result.Id);


    }

    [Fact]
    public void Update_ShouldUpdateOneReview()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        var user = new UserEntity
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test"
        };
        _context.Products.Add(product);
        _context.Users.Add(user);
        _context.SaveChanges();
        var reviewEntity = new ReviewEntity { Id = 1, Comment = "test", Product = product, User = user };
        reviewRepository.Create(reviewEntity);

        // Act
        reviewEntity.Comment = "Test comment";
        reviewRepository.Update(x => x.Id == reviewEntity.Id, reviewEntity);
        var result = reviewRepository.Get(x => x.Id == reviewEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test comment", result.Comment);



    }


    [Fact]
    public void Delete_ShouldDeleteOneReview()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context);
        var product = new ProductEntity
        {
            ProductName = "Test Product",
            Price = 100.0m,
            Description = "This is a test product",
            CategoryId = 1,
            Category = new CategoryEntity { CategoryName = "Test category" }
        };

        var user = new UserEntity
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test"
        };
        _context.Products.Add(product);
        _context.Users.Add(user);
        _context.SaveChanges();
        var reviewEntity = new ReviewEntity { Id = 1, Comment = "test", Product = product, User = user };
        reviewRepository.Create(reviewEntity);

        // Act
        reviewRepository.Delete(x => x.Id == reviewEntity.Id);
        var result = reviewRepository.Get(x => x.Id == reviewEntity.Id);

        // Assert
        Assert.Null(result);



    }
}
