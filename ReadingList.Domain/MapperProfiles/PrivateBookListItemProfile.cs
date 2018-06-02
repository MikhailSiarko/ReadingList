using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.MapperProfiles
{
    public class PrivateBookListItemProfile : Profile
    {
        public PrivateBookListItemProfile()
        {
            CreateMap<PrivateBookListItem, PrivateBookListItemDto>().ForMember(i => i.ReadingTimeInSeconds,
                t => t.MapFrom(d => d.ReadingTime.TotalSeconds));
        }
    }
}