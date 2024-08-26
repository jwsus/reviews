using Microsoft.AspNetCore.Mvc;
using CourseReviewAPI.Data;
using CourseReviewAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

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
    [SwaggerOperation(Summary = "Create a new review", Description = "Creates a new review with the provided details.")]
    [SwaggerResponse(200, "Returns the created review")]
    [SwaggerResponse(400, "If the input data is invalid")]
    public async Task<IActionResult> CreateReview([FromBody] ReviewCreateDTO review)
    {
        if (review == null)
        {
            return BadRequest();
        }

        var createdReview = await _reviewService.CreateReview(review);
        return Ok(createdReview);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a review by ID", Description = "Retrieves a specific review by its unique ID.")]
    [SwaggerResponse(200, "Returns the review details")]
    [SwaggerResponse(404, "If the review is not found")]
    public async Task<IActionResult> GetReviewById(Guid id)
    {
        try
        {
            var reviewDTO = await _reviewService.GetReviewById(id);
            return Ok(reviewDTO);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a review", Description = "Updates the details of an existing review.")]
    [SwaggerResponse(204, "Review was updated successfully")]
    [SwaggerResponse(400, "If the input data is invalid")]
    [SwaggerResponse(500, "An error occurred while updating the review")]
    public async Task<IActionResult> UpdateReview(Guid id, [FromBody] ReviewUpdateDTO reviewUpdateDTO)
    {
        if (reviewUpdateDTO == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            await _reviewService.UpdateReview(id, reviewUpdateDTO);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the review.");
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a review", Description = "Deletes a specific review by its unique ID.")]
    [SwaggerResponse(204, "Review was deleted successfully")]
    [SwaggerResponse(404, "If the review is not found")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        try
        {
            await _reviewService.DeleteReview(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
