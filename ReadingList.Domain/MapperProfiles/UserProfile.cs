using ReadingList.Models.Read;
using Profile = AutoMapper.Profile;
using User = ReadingList.Models.Write.Identity.User;

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