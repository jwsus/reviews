using CourseReviewAPI.Data;
using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;
using Microsoft.EntityFrameworkCore;

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


    public async Task<Student> CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

  public async Task<PagedResult<StudentDTO>> GetAllStudents(int pageNumber, int pageSize)
  {
      var students = await _context.Students
          .Where(c => !c.IsDeleted)
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .Select(c => new StudentDTO
              {
                  Id = c.Id,
                  Name = c.Name,
                  Email = c.Email
              })
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();


      var totalRecords = await _context.Students
          .Where(c => !c.IsDeleted)
          .CountAsync();

      return new PagedResult<StudentDTO>
      {
          TotalCount = totalRecords,
          PageNumber = pageNumber,
          PageSize = pageSize,
          Items = students
      };
  }

  public async Task<StudentDTO?> GetStudentById(Guid studentId)
    {
        return await _context.Students 
              .Where(c => c.Id == studentId)
              .Select(c => new StudentDTO
              {
                  Id = c.Id,
                  Name = c.Name,
                  Email = c.Email
              })
              .FirstOrDefaultAsync();
    }

    public async Task SoftDeleteStudent(Guid studentId)
    {
        var student = new Student { Id = studentId, IsDeleted = true }; 

        _context.Students.Attach(student);

        _context.Entry(student).Property(c => c.IsDeleted).IsModified = true;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> StudentExists(Guid studentId)
    {
        return await _context.Students.AnyAsync(c => c.Id == studentId && !c.IsDeleted);
    }

    public async Task UpdateStudent(Guid studentId, StudentUpdateDTO updateDto)
    {
        var student = new Student { Id = studentId };

        _context.Students.Attach(student);

        if (updateDto.Name != null)
        {
            student.Name = updateDto.Name;
            _context.Entry(student).Property(x => x.Name).IsModified = true;
        }

        if (updateDto.Email != null)
        {
            student.Email = updateDto.Email;
            _context.Entry(student).Property(x => x.Email).IsModified = true;
        }

        if (updateDto.Reactivate != null)
        {
            student.IsDeleted = !updateDto.Reactivate.Value;
            _context.Entry(student).Property(x => x.IsDeleted).IsModified = true;
        }

        await _context.SaveChangesAsync();
    }
}
