using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                .ForMember(d => d.JsonFields, m => m.MapFrom(b => JObject.FromObject(b,
                    JsonSerializer.Create(new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    }))))
                .ReverseMap()
                .ConvertUsing<BookListConverter>();
            CreateMap<PrivateBookListDto, BookList>().ReverseMap();
        }
    }
}