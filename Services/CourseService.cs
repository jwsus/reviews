using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;
using CourseReviewAPI.Repositories;
using System.Threading.Tasks;

namespace CourseReviewAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            return await _courseRepository.CreateCourseAsync(course);
        }

        // Outros métodos como Update, Delete, Get etc.
    }
}
