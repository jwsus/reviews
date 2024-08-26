using AutoMapper;
using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;

namespace CourseReviewAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<CourseDto> CreateCourse(CourseCreateDto courseCreateDto)
        {
            var course = new Course
            {
                Name = courseCreateDto.Name,
                Description = courseCreateDto.Description
            };

            _courseRepository.Add(course);
            _courseRepository.SaveChanges();

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
            };
        }

        public async Task SoftDeleteCourse(Guid courseId)
        {
            var exists = await _courseRepository.CourseExists(courseId);
            if (exists)
            {
                await _courseRepository.SoftDeleteCourse(courseId);
            }
            else
            {
                throw new KeyNotFoundException("O curso informado não foi encontrado");
            }
        }

        public async Task UpdateCourseAsync(Guid courseId, CourseUpdateDTO courseUpdateDto)
        {
            var exists = await _courseRepository.CourseExists(courseId);

            if (exists == null)
            {
                throw new KeyNotFoundException("Course not found");
            }
            else 
            {
                await _courseRepository.UpdateCourseAsync(courseId, courseUpdateDto);
            }
        }

        public async Task<CourseDto> GetCourseByIdAsync(Guid courseId)
        {
            var courseDto = await _courseRepository.GetCourseById(courseId);

            if (courseDto == null)
            {
                throw new KeyNotFoundException("Course informado não foi encontrado");
            }

            return courseDto;
        }

        public async Task<PagedResult<CourseWithReviewsDTO>> GetCoursesWithReviews(int pageNumber, int pageSize)
        {
            var coursesWithReviews = await _courseRepository.GetCoursesWithReviews(pageNumber,  pageSize);
            return coursesWithReviews;
        }
  }
}
