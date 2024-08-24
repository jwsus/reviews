using System.ComponentModel.DataAnnotations;

public class ReviewCreateDTO
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Stars { get; set; } 
    public string? Comment { get; set; }
}
