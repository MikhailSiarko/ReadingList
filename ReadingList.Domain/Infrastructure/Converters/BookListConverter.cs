﻿using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
using ReadingList.Domain.DTO.BookList;
using ReadingList.ReadModel.Models;

namespace ReadingList.Domain.Infrastructure.Converters
{
    public class SharedBookListConverter : ITypeConverter<SharedBookListRm, SharedBookListDto>
    {
        public SharedBookListDto Convert(SharedBookListRm source, SharedBookListDto destination, ResolutionContext context)
        {
            var deserialized = string.IsNullOrEmpty(source.JsonFields)
                ? null
                : JsonConvert.DeserializeObject<SharedBookListDto>(source.JsonFields);
            return new SharedBookListDto
            {
                Id = source.Id,
                Name = source.Name,
                OwnerId = source.OwnerId,
                Tags = deserialized?.Tags ?? new List<string>()
            };
        }
    }
}