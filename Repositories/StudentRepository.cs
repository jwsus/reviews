using CourseReviewAPI.Data;
using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Add(Student student)
    {
        _context.Students.Add(student);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public Task<StudentDTO?> GetStudentById(Guid studentId)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteStudent(Guid studentId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> StudentExists(Guid studentId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStudentAsync(Guid studentId, StudentCreateDTO updateDto)
    {
        throw new NotImplementedException();
    }
}
