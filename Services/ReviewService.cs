using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;

namespace CourseReviewAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDTO> CreateReview(ReviewCreateDTO reviewCreateDTO)
        {
            var review = new Review
            {
                Comment = reviewCreateDTO.Comment,
                Stars = reviewCreateDTO.Stars,
                CourseId = reviewCreateDTO.CourseId,
                StudentId = reviewCreateDTO.StudentId
            };

            _reviewRepository.Add(review);
            _reviewRepository.SaveChanges();

            return new ReviewDTO
            {
                Comment = review.Comment,
                Stars = review.Stars,
                CreatedAt = review.CreatedAt
            };
        }
    }
}

