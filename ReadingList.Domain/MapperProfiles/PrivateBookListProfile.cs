using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.ReadModel.Models;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.MapperProfiles
{
    public class PrivateBookListProfile : Profile
    {
        public PrivateBookListProfile()
        {
            CreateMap<PrivateBookListRm, PrivateBookListDto>();
            CreateMap<PrivateBookListItemRm, PrivateBookListItemDto>();
            CreateMap<BookListWm, PrivateBookListDto>(MemberList.None).ForMember(dto => dto.Type,
                expression => expression.MapFrom(wm => (int) wm.Type));
            CreateMap<PrivateBookListItemWm, PrivateBookListItemDto>().ForMember(dto => dto.Status,
                expression => expression.MapFrom(wm => (int) wm.Status));
        }
    }
}