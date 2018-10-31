using ReadingList.Domain.Entities.Identity;
using ReadingList.Application.DTO.User;
using Profile = AutoMapper.Profile;

namespace ReadingList.Application.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserIdentityDto>()
                .ForMember(u => u.Role, expression => expression.MapFrom(i => i.Role.Name));
        } 
    }
}