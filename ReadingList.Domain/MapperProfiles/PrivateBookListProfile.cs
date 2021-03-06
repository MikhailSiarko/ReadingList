using AutoMapper;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.MapperProfiles
{
    public class PrivateBookListProfile : Profile
    {
        public PrivateBookListProfile()
        {
            CreateMap<BookList, PrivateBookListDto>(MemberList.None).ForMember(dto => dto.Type,
                expression => expression.MapFrom(wm => (int) wm.Type));
            CreateMap<PrivateBookListItem, PrivateBookListItemDto>()
                .ForMember(dto => dto.ListId, expression => expression.MapFrom(item => item.BookListId))
                .ForMember(dto => dto.Status, expression => expression.MapFrom(wm => (int) wm.Status))
                .ForMember(dto => dto.Author, expression => expression.MapFrom(item => item.Book.Author))
                .ForMember(dto => dto.Title, expression => expression.MapFrom(item => item.Book.Title))
                .ForMember(dto => dto.Genre, expression => expression.MapFrom(item => item.Book.Genre.Name));
        }
    }
}