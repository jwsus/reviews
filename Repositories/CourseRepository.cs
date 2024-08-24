using AutoMapper;
using CourseReviewAPI.Data;
using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CourseReviewAPI.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public async Task<Course> CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> CourseExists(Guid courseId)
        {
            return await _context.Courses.AnyAsync(c => c.Id == courseId && !c.IsDeleted);
        }

        public async Task<CourseDto?> GetCourseById(Guid courseId)
        {
            return await _context.Courses 
              .Where(c => c.Id == courseId)
              .Select(c => new CourseDto
              {
                  Id = c.Id,
                  Name = c.Name,
                  Description = c.Description
              })
              .FirstOrDefaultAsync();
        }

        public async Task SoftDeleteCourse(Guid courseId)
        {
            var course = new Course { Id = courseId, IsDeleted = true }; 

            _context.Courses.Attach(course);

            _context.Entry(course).Property(c => c.IsDeleted).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Guid courseId, CourseUpdateDTO updateDto)
        {
            var course = new Course { Id = courseId };

            _context.Courses.Attach(course);

            if (updateDto.Name != null)
            {
                course.Name = updateDto.Name;
                _context.Entry(course).Property(x => x.Name).IsModified = true;
            }

            if (updateDto.Description != null)
            {
                course.Description = updateDto.Description;
                _context.Entry(course).Property(x => x.Description).IsModified = true;
            }

            if (updateDto.Reactivate != null)
            {
                course.IsDeleted = !updateDto.Reactivate.Value;
                _context.Entry(course).Property(x => x.IsDeleted).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<CourseWithReviewsDTO>> GetCoursesWithReviews(int pageNumber, int pageSize)
        {
            var courseWithReviewsDTO = await _context.Courses
                .Where(c => !c.IsDeleted) 
                .Include(c => c.Reviews) 
                .ThenInclude(r => r.Student)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CourseWithReviewsDTO
                {
                    CourseId = c.Id,
                    CourseName = c.Name,
                    CourseDescription = c.Description,
                    Reviews = c.Reviews
                    .Select(r => new ReviewDTO
                    {
                        ReviewId = r.Id,
                        StudentId = r.StudentId,
                        StudentName = r.Student.Name,
                        Stars = r.Stars,
                        Comment = r.Comment,
                        CreatedAt = r.CreatedAt
                    }).ToList()
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var totalRecords = await _context.Courses
                .Where(c => !c.IsDeleted)
                .CountAsync();

            return new PagedResult<CourseWithReviewsDTO>
            {
                TotalCount = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = courseWithReviewsDTO
            };
        }
  }
}
