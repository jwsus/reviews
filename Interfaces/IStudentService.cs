namespace CourseReviewAPI.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDTO> CreateStudent(StudentCreateDTO  student);
        Task<StudentDTO> GetStudentById(Guid studentId);
        Task SoftDeleteStudent(Guid studentId);
        Task UpdateStudent(Guid studentId, StudentUpdateDTO studentCreateDTO);
        Task<PagedResult<StudentDTO>> GetAllStudents(int pageNumber, int pageSize);
    }
}