using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.MapperProfiles
{
    public class SharedBookListProfile : Profile
    {
        public SharedBookListProfile()
        {
            CreateMap<SharedBookListRm, SharedBookListDto>();
            CreateMap<SharedBookListItemRm, SharedBookListItemDto>();
        }
    }
}