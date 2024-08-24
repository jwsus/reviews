namespace CourseReviewAPI.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDTO> CreateStudent(StudentCreateDTO  student);
    }
}