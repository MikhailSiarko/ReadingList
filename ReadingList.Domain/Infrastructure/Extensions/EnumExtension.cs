using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class EnumExtension
    {
        public static string ToStringFromDescription(this Enum @enum)
        {
            var fieldInfo = @enum.GetType().GetField(@enum.ToString());
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false);
            return attribute != null ? attribute.Description : @enum.ToString();
        }

        public static IEnumerable<NameValuePair> ToNameValuePairs(this Enum @enum)
        {
            var enumType = @enum.GetType();

            var values = Enum.GetValues(enumType);

            var statuses = new List<NameValuePair>();

            foreach (var value in values)
            {
                statuses.Add(new NameValuePair
                {
                    Value = value,
                    Name = Enum.GetName(enumType, value)
                });
            }

            return statuses;
        }
    }
}