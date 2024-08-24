using CourseReviewAPI.Models;

namespace CourseReviewAPI.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        void Add(Student student);
        Task<Student> CreateStudentAsync(Student student);
        Task SoftDeleteStudent(Guid studentId);
        Task<bool> StudentExists(Guid studentId);
        Task UpdateStudentAsync(Guid studentId, StudentCreateDTO updateDto);
        Task<StudentDTO?> GetStudentById(Guid studentId);
    }
}
