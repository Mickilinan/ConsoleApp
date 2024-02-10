using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;
using DbAssignment.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class ReviewService_Tests
{

    private readonly DataContext2 _context2 =
     new(new DbContextOptionsBuilder<DataContext2>()
         .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);

    private readonly DataContext _context =
     new(new DbContextOptionsBuilder<DataContext>()
          .UseInMemoryDatabase($"{Guid.NewGuid()}")
          .Options);

    [Fact]
    public void CreateReview_ShouldReturnReview_WhenReviewIsCreated()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var userRepository = new UserRepository(_context);
        var reviewService = new ReviewService(reviewRepository, productRepository, userRepository);

        string comment = "Test comment";
        int userId = 1;
        int productId = 1;



        // Act
        var result = reviewService.CreateReview(comment, userId, productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comment, result.Comment);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(productId, result.ProductId);
    }

    [Fact]
    public void GetAllReviews_ShouldReturnAllReviews()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var userRepository = new UserRepository(_context);
        var reviewService = new ReviewService(reviewRepository, productRepository, userRepository);

        int userId = 1;
        int productId = 1;
        reviewService.CreateReview("Test comment", userId, productId);
        reviewService.CreateReview("Test comment", userId, productId);
        reviewService.CreateReview("Test comment", userId, productId);


        // Act
        var result = reviewService.GetAllReviews();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void GetReviewById_ShouldReturnReview_WhenReviewExists()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var userRepository = new UserRepository(_context);
        var reviewService = new ReviewService(reviewRepository, productRepository, userRepository);

        string comment = "Test comment";
        int userId = 1;
        int productId = 1;

        var createdReview = reviewService.CreateReview(comment, userId, productId);

        // Act
        var result = reviewService.GetReviewById(createdReview.Id);

        // Assert
        Assert.Equal(createdReview.Id, result.Id);
    }

    [Fact]
    public void GetReviewById_ShouldReturnNull_WhenReviewDoesNotExist()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var userRepository = new UserRepository(_context);
        var reviewService = new ReviewService(reviewRepository, productRepository, userRepository);

        // Act
        var result = reviewService.GetReviewById(999);

        // Assert
        Assert.Null(result);
    }

   
    [Fact]
    public void DeleteReview_ShouldReturnTrue_WhenReviewIsDeleted()
    {
        // Arrange
        var reviewRepository = new ReviewRepository(_context2);
        var productRepository = new ProductRepository(_context);
        var userRepository = new UserRepository(_context);
        var reviewService = new ReviewService(reviewRepository, productRepository, userRepository);

        string comment = "Test comment";
        int userId = 1;
        int productId = 1;

        var createdReview = reviewService.CreateReview(comment, userId, productId);

        // Act
        reviewService.DeleteReview(createdReview.Id);

        // Assert
        var deletedProductImage = reviewService.GetReviewById(createdReview.Id);
        Assert.Null(deletedProductImage);
    }
}
