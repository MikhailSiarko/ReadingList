using AutoMapper;
using ReadingList.Domain.Entities;
using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.MapperProfiles
{
    public class PrivateBookListProfile : Profile
    {
        public PrivateBookListProfile()
        {
            CreateMap<BookList, PrivateBookListDto>(MemberList.None).ForMember(dto => dto.Type,
                expression => expression.MapFrom(wm => (int) wm.Type));
            CreateMap<PrivateBookListItem, PrivateBookListItemDto>().ForMember(dto => dto.Status,
                expression => expression.MapFrom(wm => (int) wm.Status));
        }
    }
}