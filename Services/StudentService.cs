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

    public async Task<PagedResult<StudentDTO>> GetAllStudents(int pageNumber, int pageSize)
    {
        return await _studentRepository.GetAllStudents(pageNumber, pageSize);
    }

    public async Task<StudentDTO> GetStudentById(Guid studentId)
      {
          StudentDTO studentDTO = await _studentRepository.GetStudentById(studentId);

          if (studentDTO == null)
          {
              throw new KeyNotFoundException("Estudante informado não foi encontrado");
          }

          return studentDTO;
      }

      public async Task SoftDeleteStudent(Guid studentId)
      {
          var exists = await _studentRepository.StudentExists(studentId);
          if (exists)
          {
              await _studentRepository.SoftDeleteStudent(studentId);
          }
          else
          {
              throw new KeyNotFoundException("O estudante informado não foi encontrado");
          }
      }

      public async Task UpdateStudent(Guid studentId, StudentUpdateDTO studentCreateDTO)
      {
          var exists = await _studentRepository.StudentExists(studentId);

          if (exists == null)
          {
              throw new KeyNotFoundException("Review not found");
          }
          else 
          {
              await _studentRepository.UpdateStudent(studentId, studentCreateDTO);
          }
      }
  }
}

