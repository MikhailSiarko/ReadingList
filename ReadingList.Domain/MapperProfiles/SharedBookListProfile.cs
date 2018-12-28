using System.Linq;
using AutoMapper;
using ReadingList.Models.Read;
using ReadingList.Models.Write;

namespace ReadingList.Domain.MapperProfiles
{
    public class SharedBookListProfile : Profile
    {
        public SharedBookListProfile()
        {
            CreateMap<BookList, SharedBookListPreviewDto>(MemberList.None)
                .ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.SharedBookListTags.Select(t => t.Tag.Name).ToList()))
                .ForMember(dto => dto.Type, expression => expression.MapFrom(wm => (int) wm.Type));

            CreateMap<BookList, SharedBookListDto>(MemberList.None)
                .ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.SharedBookListTags.Select(t => new TagDto
                    {
                        Id = t.Tag.Id,
                        Name = t.Tag.Name
                    }).ToList()))
                .ForMember(dto => dto.Moderators, expression => expression.MapFrom(list =>
                    list.BookListModerators.Select(m => new ModeratorDto
                    {
                        Id = m.UserId,
                        Login = m.User.Login
                    }).ToList()))
                .ForMember(dto => dto.Type, expression => expression.MapFrom(wm => (int) wm.Type));

            CreateMap<SharedBookListItem, SharedBookListItemDto>()
                .ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.Book.BookTags.Select(t => t.Tag.Name).ToList()))
                .ForMember(dto => dto.ListId, expression => expression.MapFrom(item => item.BookListId))
                .ForMember(dto => dto.Author, expression => expression.MapFrom(item => item.Book.Author))
                .ForMember(dto => dto.Title, expression => expression.MapFrom(item => item.Book.Title))
                .ForMember(dto => dto.GenreId, expression => expression.MapFrom(item => item.Book.GenreId));
        }
    }
}