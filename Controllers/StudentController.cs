using Microsoft.AspNetCore.Mvc;
using CourseReviewAPI.Data;
using CourseReviewAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Implementar endpoints CRUD aqui
}
