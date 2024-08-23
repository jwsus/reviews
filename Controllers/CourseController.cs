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

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }

            var createdCourse = await _courseService.CreateCourseAsync(course);
            return Ok(createdCourse);
        }

        // Exemplo de rota para buscar curso por ID
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetCourseById(int id)
        // {
        //     var course = await _courseService.GetCourseByIdAsync(id);
        //     if (course == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(course);
        // }

        // Outros métodos como Update, Delete etc.
    }
}
