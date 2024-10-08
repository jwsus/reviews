using Microsoft.Extensions.DependencyInjection;
using CourseReviewAPI.Interfaces;
using CourseReviewAPI.Services;
using CourseReviewAPI.Repositories;
using CourseReviewAPI.Models;
using Microsoft.EntityFrameworkCore;
using CourseReviewAPI.Data;

namespace CourseReviewAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            /* Init Zone: Services configuration */

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IReviewService, ReviewService>();

            /* End Zone: Services configuration */

            
            /* Init Zone: Repository configuration */

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            /* End Zone: Repository configuration */
        }
    }
}
