using CourseReviewAPI.Models;

namespace CourseReviewAPI.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        void Add(Student student);
        Task<Student> CreateStudent(Student student);
        Task SoftDeleteStudent(Guid studentId);
        Task<bool> StudentExists(Guid studentId);
        Task UpdateStudent(Guid studentId, StudentUpdateDTO updateDto);
        Task<StudentDTO?> GetStudentById(Guid studentId);
        Task<PagedResult<StudentDTO>> GetAllStudents(int pageNumber, int pageSize);
    }
}
