using Newtonsoft.Json;
using ReadingList.Domain.DTO.BookList;
using ReadingList.Domain.Infrastructure.Converters;
using ReadingList.WriteModel.Models;
using Profile = AutoMapper.Profile;

namespace ReadingList.Domain.MapperProfiles
{
    public class BookListProfile : Profile
    {
        public BookListProfile()
        {
            CreateMap<SharedBookListDto, BookList>()
                .ForMember(d => d.JsonFields, m => m.MapFrom(b => JsonConvert.SerializeObject(b, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                })))
                .ReverseMap().ConvertUsing<BookListConverter>();
            CreateMap<PrivateBookListDto, BookList>().ReverseMap();
        }
    }
}