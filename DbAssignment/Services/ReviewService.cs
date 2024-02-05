using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class ReviewService
{

    private readonly ReviewRepository _reviewRepository;
    private readonly BaseRepo<DataContext, ProductEntity> _productRepository;
    private readonly BaseRepo<DataContext, UserEntity> _userRepository;

    public ReviewService(ReviewRepository reviewRepository, BaseRepo<DataContext, ProductEntity> productRepository, BaseRepo<DataContext, UserEntity> userRepository)
    {
        _reviewRepository = reviewRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }


    public ReviewEntity CreateReview(string comment, int userId, int productId)
    {



        var reviewEntity = new ReviewEntity
        {
            Comment = comment,
            UserId = userId,
            ProductId = productId
        };

        reviewEntity = _reviewRepository.Create(reviewEntity);

        return reviewEntity;

    }

    public void DeleteReview(int Id)
    {
        _reviewRepository.Delete(x => x.Id == Id);

    }

    public ReviewEntity GetReviewWithProductAndUser(int reviewId)
    {
        // Get a review
        var review = _reviewRepository.Get(x => x.Id == reviewId);

        // Get the related product
        var product = _productRepository.Get(x => x.Id == review.ProductId);

        // Get the related user
        var user = _userRepository.Get(x => x.Id == review.UserId);

        // Attach the product and user to the review
        review.Product = product;
        review.User = user;

        return review;
    }
}
