using AutoMapper;
using TestTask.Data.Dtos;
using TestTask.Data.Entitys;
using TestTask.Data.Interfaces;

namespace TestTask.Data.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles)); 
        }
    }
}
