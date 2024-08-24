using System.ComponentModel.DataAnnotations;

namespace CourseReviewAPI.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Stars { get; set; } 
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
