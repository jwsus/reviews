using CourseReviewAPI.Models;

namespace CourseReviewAPI.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        void Add(Review review);
        Task<Review> CreateReview(Review review);
        Task DeleteReview(Guid reviewId);
        Task<bool> ReviewExists(Guid reviewId);
        Task UpdateReview(Guid reviewId, ReviewUpdateDTO updateDto);
        Task<ReviewDTO?> GetReviewById(Guid reviewId);
    }
}
