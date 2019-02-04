using ReadingList.Models.Read;
using ReadingList.Models.Write.Identity;
using Profile = AutoMapper.Profile;

namespace ReadingList.Domain.MapperProfiles
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