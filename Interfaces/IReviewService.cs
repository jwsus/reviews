namespace CourseReviewAPI.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDTO> CreateReview(ReviewCreateDTO review);
        Task DeleteReview(Guid reviewId);
        Task UpdateReview(Guid reviewId, ReviewUpdateDTO reviewUpdateDto);
        Task<ReviewDTO> GetReviewById(Guid reviewId);
    }
}