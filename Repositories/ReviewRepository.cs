using CourseReviewAPI.Data;
using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;

public class ReviewRepository : BaseRepository<ReviewRepository>, IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Add(Review review)
    {
        _context.Reviews.Add(review);
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task<Review> CreateReviewAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public Task<ReviewDTO?> GetReviewById(Guid reviewId)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteReview(Guid reviewId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReviewExists(Guid reviewId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateReviewAsync(Guid reviewId, ReviewCreateDTO updateDto)
    {
        throw new NotImplementedException();
    }
}
