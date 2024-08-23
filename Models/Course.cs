namespace CourseReviewAPI.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public bool IsDeleted { get; set; }
    }
}
