using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Models;

namespace CourseReviewAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDTO> CreateStudent(StudentCreateDTO studentCreateDTO)
        {
            var student = new Student
            {
                Name = studentCreateDTO.Name,
                Email = studentCreateDTO.Email
            };

            _studentRepository.Add(student);
            _studentRepository.SaveChanges();

            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
            };
        }
    }
}

