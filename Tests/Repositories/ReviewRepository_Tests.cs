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
        var reviewEntity = new ReviewEntity { Id = 1, Comment = "Comment test" };


        // Act
        var result = reviewRepository.Create(reviewEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }
}
