using Microsoft.AspNetCore.Mvc;
using CourseReviewAPI.Data;
using CourseReviewAPI.Models;
using CourseReviewAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;

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
    [SwaggerOperation(Summary = "Create a new student", Description = "Creates a new student with the provided details.")]
    [SwaggerResponse(200, "Returns the created student")]
    [SwaggerResponse(400, "If the input data is invalid")]
    public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDTO student)
    {
        if (student == null)
        {
            return BadRequest();
        }

        var createdStudent = await _studentService.CreateStudent(student);
        return Ok(createdStudent);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a student by ID", Description = "Retrieves a specific student by their unique ID.")]
    [SwaggerResponse(200, "Returns the student details")]
    [SwaggerResponse(404, "If the student is not found")]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        try
        {
            var studentDto = await _studentService.GetStudentById(id);
            return Ok(studentDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Soft delete a student", Description = "Marks a student as deleted by setting a flag instead of actually deleting the record.")]
    [SwaggerResponse(204, "Student was deleted successfully")]
    [SwaggerResponse(404, "If the student is not found")]
    public async Task<IActionResult> SoftDeleteStudent(Guid id)
    {
        try
        {
            await _studentService.SoftDeleteStudent(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a student", Description = "Updates the details of an existing student.")]
    [SwaggerResponse(204, "Student was updated successfully")]
    [SwaggerResponse(400, "If the input data is invalid")]
    [SwaggerResponse(500, "An error occurred while updating the student")]
    public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentUpdateDTO studentUpdateDTO)
    {
        if (studentUpdateDTO == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            await _studentService.UpdateStudent(id, studentUpdateDTO);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while updating the student.");
        }
    }

    [HttpGet("get-all-students")]
    [SwaggerOperation(Summary = "Get all students with pagination", Description = "Retrieves a paginated list of all students.")]
    [SwaggerResponse(200, "Returns the list of students")]
    [SwaggerResponse(404, "If no students are found")]
    public async Task<IActionResult> GetAllStudents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var allStudents = await _studentService.GetAllStudents(pageNumber, pageSize);

        if (allStudents == null || !allStudents.Items.Any())
        {
            return NotFound("No students found.");
        }

        return Ok(allStudents);
    }
}
