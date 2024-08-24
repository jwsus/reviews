using Microsoft.AspNetCore.Mvc;
using CourseReviewAPI.Data;
using CourseReviewAPI.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IReviewService _reviewService;

    public ReviewsController(ApplicationDbContext context, IReviewService reviewService)
    {
        _context = context;
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview ([FromBody] ReviewCreateDTO review)
    {
        if (review == null)
        {
            return BadRequest();
        }

        var createdCourse = await _reviewService.CreateReview(review);
        return Ok(createdCourse);
    }
}
