using AutoMapper;
using ReadingList.Domain.DTO.BookList;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.MapperProfiles
{
    public class PrivateBookListProfile : Profile
    {
        public PrivateBookListProfile()
        {
            CreateMap<PrivateBookListRm, PrivateBookListDto>();
            CreateMap<PrivateBookListItemRm, PrivateBookListItemDto>();
        }
    }
}