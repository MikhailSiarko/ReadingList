using AutoMapper;
using ReadingList.Domain.DTO.User;
using ReadingList.ReadModel.Models;

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