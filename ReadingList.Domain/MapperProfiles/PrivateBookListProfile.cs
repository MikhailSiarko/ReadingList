using AutoMapper;
using ReadingList.Domain.Models.DAO;
using ReadingList.Domain.Models.DTO.BookLists;

namespace ReadingList.Domain.MapperProfiles
{
    public class PrivateBookListProfile : Profile
    {
        public PrivateBookListProfile()
        {
            CreateMap<BookList, PrivateBookListDto>(MemberList.None).ForMember(dto => dto.Type,
                expression => expression.MapFrom(wm => (int) wm.Type));
            CreateMap<PrivateBookListItem, PrivateBookListItemDto>()
                .ForMember(dto => dto.Status, expression => expression.MapFrom(wm => (int) wm.Status))
                .ForMember(dto => dto.Author, expression => expression.MapFrom(item => item.Book.Author))
                .ForMember(dto => dto.Title, expression => expression.MapFrom(item => item.Book.Title))
                .ForMember(dto => dto.GenreId, expression => expression.MapFrom(item => item.Book.GenreId));
        }
    }
}