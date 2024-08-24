using AutoMapper;
using CourseReviewAPI.Models;

namespace CourseReviewAPI.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseUpdateDTO, Course>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
