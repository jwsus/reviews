using CourseReviewAPI.Models;
using System.Threading.Tasks;

namespace CourseReviewAPI.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        void Add(Course course);
        Task<Course> CreateCourse(Course course);
        Task SoftDeleteCourse(Guid courseId);
        Task<bool> CourseExists(Guid courseId);
        Task UpdateCourseAsync(Guid courseId, CourseUpdateDTO updateDto);
        Task<CourseDto?> GetCourseById(Guid courseId);
        Task<PagedResult<CourseWithReviewsDTO>> GetCoursesWithReviews(int pageNumber, int pageSize);
    }
}
