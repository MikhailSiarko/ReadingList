using System;
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
    }
}