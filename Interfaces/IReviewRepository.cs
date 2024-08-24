using CourseReviewAPI.Models;

namespace CourseReviewAPI.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        void Add(Review review);
        
        // Task<Student> CreateStudentAsync(Student student);
        // Task SoftDeleteStudent(Guid studentId);
        // Task<bool> StudentExists(Guid studentId);
        // Task UpdateStudentAsync(Guid studentId, StudentCreateDTO updateDto);
        // Task<StudentDTO?> GetStudentById(Guid studentId);
    }
}
