using CourseReviewAPI.Models;
using System.Threading.Tasks;

namespace CourseReviewAPI.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> CreateCourseAsync(Course course);
        // Outros métodos como Update, Delete, Get etc.
    }
}
