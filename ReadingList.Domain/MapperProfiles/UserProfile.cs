using ReadingList.Domain.Models.DTO.User;
using Profile = AutoMapper.Profile;
using User = ReadingList.Domain.Models.DAO.Identity.User;

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