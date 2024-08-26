using CourseReviewAPI.Data;
using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Review> CreateReview(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<ReviewDTO?> GetReviewById(Guid reviewId)
    {
        return await _context.Reviews 
              .Where(c => c.Id == reviewId)
              .Select(c => new ReviewDTO
              {
                  ReviewId = c.Id,
                  StudentId = c.StudentId,
                  StudentName = c.Student.Name,
                  Stars = c.Stars,
                  Comment = c.Comment,
                  CreatedAt = c.CreatedAt
              })
              .FirstOrDefaultAsync();
    }

    public async Task DeleteReview(Guid reviewId)
    {
        var review = new Review { Id = reviewId };  
        _context.Reviews.Remove(review);            
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ReviewExists(Guid reviewId)
    {
        return await _context.Reviews.AnyAsync(c => c.Id == reviewId);
    }

    public async Task UpdateReview(Guid reviewId, ReviewUpdateDTO updateDto)
    {
        var review = new Review { Id = reviewId };

        _context.Reviews.Attach(review);

        if (updateDto.Comment != null)
        {
            review.Comment = updateDto.Comment;
            _context.Entry(review).Property(x => x.Comment).IsModified = true;
        }

        if (updateDto.Stars != null)
        {
            review.Stars = (int)updateDto.Stars;
            _context.Entry(review).Property(x => x.Stars).IsModified = true;
        }

        await _context.SaveChangesAsync();
    }
}
