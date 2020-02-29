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
            var attribute = (DescriptionAttribute) fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), false);
            return attribute != null ? attribute.Description : @enum.ToString();
        }

        public static IEnumerable<SelectListItem<int>> ToSelectListItems(this Enum @enum)
        {
            var enumType = @enum.GetType();

            var values = Enum.GetValues(enumType);

            var items = new List<SelectListItem<int>>();

            foreach (var value in values)
            {
                items.Add(new SelectListItem<int>
                {
                    Value = (int) Convert.ChangeType(value, enumType),
                    Text = ((Enum) value).ToStringFromDescription()
                });
            }

            return items;
        }
    }
}