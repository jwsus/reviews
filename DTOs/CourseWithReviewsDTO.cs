public class CourseWithReviewsDTO
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseDescription { get; set; }
    public List<ReviewDTO> Reviews { get; set; }
}