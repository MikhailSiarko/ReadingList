using System.Linq;
using AutoMapper;
using ReadingList.Domain.Entities;
using ReadingList.Application.DTO.BookList;

namespace ReadingList.Application.MapperProfiles
{
    public class SharedBookListProfile : Profile
    {
        public SharedBookListProfile()
        {
            CreateMap<BookList, SharedBookListPreviewDto>(MemberList.None).ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.SharedBookListTags.Select(t => t.Tag.Name)))
                .ForMember(dto => dto.Type, expression => expression.MapFrom(wm => (int) wm.Type));
            
            CreateMap<BookList, SharedBookListDto>(MemberList.None).ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.SharedBookListTags.Select(t => t.Tag.Name)))
                .ForMember(dto => dto.Type, expression => expression.MapFrom(wm => (int) wm.Type));
            
            CreateMap<SharedBookListItem, SharedBookListItemDto>().ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.SharedBookListItemTags.Select(t => t.Tag.Name)))
                .ForMember(dto => dto.ListId, expression => expression.MapFrom(wm => wm.BookListId));
        }
    }
}