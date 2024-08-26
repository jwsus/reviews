using Microsoft.AspNetCore.Mvc;
using CourseReviewAPI.Models;
using CourseReviewAPI.Services;
using System.Threading.Tasks;
using CourseReviewAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

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
        [SwaggerOperation(Summary = "Get a course by ID", Description = "Retrieves a specific course by its unique ID.")]
        [SwaggerResponse(200, "Returns the course details")]
        [SwaggerResponse(404, "If the course is not found")]
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
        [SwaggerOperation(Summary = "Create a new course", Description = "Creates a new course with the provided details.")]
        [SwaggerResponse(200, "Returns the created course")]
        [SwaggerResponse(400, "If the input data is invalid")]
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
        [SwaggerOperation(Summary = "Soft delete a course", Description = "Marks a course as deleted without removing it from the database.")]
        [SwaggerResponse(204, "Course was soft deleted successfully")]
        [SwaggerResponse(404, "If the course is not found")]
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
        [SwaggerOperation(Summary = "Update a course", Description = "Updates the details of an existing course.")]
        [SwaggerResponse(204, "Course was updated successfully")]
        [SwaggerResponse(400, "If the input data is invalid")]
        [SwaggerResponse(500, "An error occurred while updating the course")]
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
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the course.");
            }
        }

        [HttpGet("courses-with-reviews")]
        [SwaggerOperation(Summary = "Get courses with reviews", Description = "Retrieves a list of courses along with their reviews, with pagination support.")]
        [SwaggerResponse(200, "Returns the list of courses with reviews")]
        [SwaggerResponse(404, "If no courses with reviews are found")]
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
