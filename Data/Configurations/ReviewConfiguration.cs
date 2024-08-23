using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CourseReviewAPI.Models;

namespace CourseReviewAPI.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            
            builder.Property(r => r.Stars)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .HasMaxLength(1000);

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            builder.HasOne(r => r.Course)
                   .WithMany(c => c.Reviews)
                   .HasForeignKey(r => r.CourseId);

            builder.HasOne(r => r.Student)
                   .WithMany()
                   .HasForeignKey(r => r.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
