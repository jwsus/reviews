namespace CourseReviewAPI.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDTO> CreateReview(ReviewCreateDTO review);
    }
}