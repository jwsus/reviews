using CourseReviewAPI.Models;
using System.Threading.Tasks;

namespace CourseReviewAPI.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourse(CourseCreateDto  course);
        Task SoftDeleteCourse(Guid courseId);
        Task UpdateCourseAsync(Guid courseId, CourseUpdateDTO courseUpdateDto);
        Task<CourseDto> GetCourseByIdAsync(Guid courseId);
        Task<PagedResult<CourseWithReviewsDTO>> GetCoursesWithReviews(int pageNumber, int pageSize);
    }
}
