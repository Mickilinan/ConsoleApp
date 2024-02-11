using DbAssignment.Contexts;
using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class ReviewService(ReviewRepository reviewRepository, BaseRepo<DataContext, ProductEntity> productRepository, BaseRepo<DataContext, UserEntity> userRepository)
{

    private readonly ReviewRepository _reviewRepository = reviewRepository;
    private readonly BaseRepo<DataContext, ProductEntity> _productRepository = productRepository;
    private readonly BaseRepo<DataContext, UserEntity> _userRepository = userRepository;

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

    public List<ReviewEntity> GetAllReviews()
    {
        return _reviewRepository.GetAll().ToList();
    }

    public ReviewEntity GetReviewById(int id)
    {
        return _reviewRepository.Get(x => x.Id == id);
    }

    public void DeleteReview(int Id)
    {
        _reviewRepository.Delete(x => x.Id == Id);

    }

    public ReviewEntity GetReviewWithProductAndUser(int reviewId)
    {
       
        var review = _reviewRepository.Get(x => x.Id == reviewId);

        
        var product = _productRepository.Get(x => x.Id == review.ProductId);

        
        var user = _userRepository.Get(x => x.Id == review.UserId);

        
        review.Product = product;
        review.User = user;

        return review;
    }
}
