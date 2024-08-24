using Microsoft.AspNetCore.Mvc;
using CourseReviewAPI.Data;
using CourseReviewAPI.Models;
using CourseReviewAPI.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStudentService _studentService;

    public StudentsController(ApplicationDbContext context, IStudentService studentService)
    {
        _context = context;
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDTO student)
    {
        if (student == null)
        {
            return BadRequest();
        }

        var createdCourse = await _studentService.CreateStudent(student);
        return Ok(createdCourse);
    }
}
