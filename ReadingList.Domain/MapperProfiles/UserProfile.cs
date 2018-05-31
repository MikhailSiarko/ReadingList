using AutoMapper;
using ReadingList.Domain.DTO.User;
using UserRm = ReadingList.ReadModel.Models.User;

namespace ReadingList.Domain.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRm, UserIdentityDto>();
        } 
    }
}