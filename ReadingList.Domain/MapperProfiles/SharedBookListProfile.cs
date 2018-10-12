using System.Linq;
using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.ReadModel.Models;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.MapperProfiles
{
    public class SharedBookListProfile : Profile
    {
        public SharedBookListProfile()
        {
            CreateMap<SimplifiedSharedBookListRm, SimplifiedSharedBookListDto>();
            CreateMap<SharedBookListItemRm, SharedBookListItemDto>();
            CreateMap<BookListWm, SimplifiedSharedBookListDto>(MemberList.None).ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.SharedBookListTags.Select(t => t.Tag.Name)))
                .ForMember(dto => dto.Type, expression => expression.MapFrom(wm => (int) wm.Type));
            CreateMap<SharedBookListItemWm, SharedBookListItemDto>().ForMember(dto => dto.Tags,
                    expression => expression.MapFrom(wm => wm.SharedBookListItemTags.Select(t => t.Tag.Name)))
                .ForMember(dto => dto.ListId, expression => expression.MapFrom(wm => wm.BookListId));
            CreateMap<SharedBookListRm, SharedListDto>(MemberList.None);
        }
    }
}