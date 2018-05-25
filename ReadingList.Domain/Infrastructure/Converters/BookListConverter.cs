using AutoMapper;
using Newtonsoft.Json;
using ReadingList.Domain.DTO.BookList;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Infrastructure.Converters
{
    public class BookListConverter : ITypeConverter<BookList, SharedBookListDto>
    {
        public SharedBookListDto Convert(BookList source, SharedBookListDto destination, ResolutionContext context)
        {
            var deserialized = JsonConvert.DeserializeObject<SharedBookListDto>(source.JsonFields);
            return new SharedBookListDto
            {
                Id = source.Id,
                Name = source.Name,
                OwnerId = source.OwnerId,
                Type = source.Type,
                Category = deserialized.Category,
                Tags = deserialized.Tags
            };
        }
    }
}