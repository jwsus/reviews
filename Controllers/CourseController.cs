using Microsoft.AspNetCore.Mvc;
using CourseReviewAPI.Models;
using CourseReviewAPI.Services;
using System.Threading.Tasks;
using CourseReviewAPI.Interfaces;

namespace CourseReviewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            try
            {
              var courseDto = await _courseService.GetCourseByIdAsync(id);

              return Ok(courseDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseCreateDto course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            var createdCourse = await _courseService.CreateCourse(course);
            return Ok(createdCourse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteCourse(Guid id)
        {
            try
            {
                await _courseService.SoftDeleteCourse(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] CourseUpdateDTO courseUpdateDTO)
        {
            if (courseUpdateDTO == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _courseService.UpdateCourseAsync(id, courseUpdateDTO);
    
                return NoContent();  
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the course.");
            }
        }

        [HttpGet("courses-with-reviews")]
        public async Task<IActionResult> GetCoursesWithReviews([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var coursesWithReviews = await _courseService.GetCoursesWithReviews(pageNumber, pageSize);

            if (coursesWithReviews == null || !coursesWithReviews.Items.Any())
            {
                return NotFound("No courses found with reviews.");
            }

            return Ok(coursesWithReviews);
        }
    }
}
