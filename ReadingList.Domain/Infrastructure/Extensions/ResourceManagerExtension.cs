using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class ResourceManagerExtension
    {
        public static bool TryGetValueByName(this ResourceManager resourceManager, string name, out string value)
        {
            return resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true)
                .Cast<DictionaryEntry>()
                .ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString())
                .TryGetValue(name, out value);
        }
        
        public static bool TryGetNotExistExceptionMessageByTypeName(this ResourceManager resourceManager, Type objectType, out string value)
        {
            return resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true)
                .Cast<DictionaryEntry>()
                .ToDictionary(entry => entry.Key.ToString(), entry => entry.Value.ToString())
                .TryGetValue($"ObjectNotExist_{objectType.Name}", out value);
        }
    }
}