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
                CreatedAt = review.CreatedAt,
                ReviewId = review.Id,
                StudentId = review.StudentId
            };
        }

    public async Task<ReviewDTO> GetReviewById(Guid reviewId)
    {
        ReviewDTO reviewDto = await _reviewRepository.GetReviewById(reviewId);

        if (reviewDto == null)
        {
            throw new KeyNotFoundException("Course informado não foi encontrado");
        }

        return reviewDto;
}

    public async Task DeleteReview(Guid reviewId)
    {
        var exists = await _reviewRepository.ReviewExists(reviewId);
        if (exists)
        {
            await _reviewRepository.DeleteReview(reviewId);
        }
        else
        {
            throw new KeyNotFoundException("O curso informado não foi encontrado");
        }
    }

    public async Task UpdateReview(Guid reviewId, ReviewUpdateDTO reviewUpdateDto)
    {
        var exists = await _reviewRepository.ReviewExists(reviewId);

        if (exists == null)
        {
            throw new KeyNotFoundException("Review not found");
        }
        else 
        {
            await _reviewRepository.UpdateReview(reviewId, reviewUpdateDto);
        }
    }

  }
}

