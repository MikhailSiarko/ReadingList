using ReadingList.Resources;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string F(this string source, params object[] args) => string.Format(source, args);

        public static string TrimModelSuffix(this string source)
        {
            if (source.EndsWith(CommonResources.ReadModelSuffix))
                return RemoveModelSuffix(source, CommonResources.ReadModelSuffix);

            return source.EndsWith(CommonResources.WriteModelSuffix)
                ? RemoveModelSuffix(source, CommonResources.WriteModelSuffix)
                : source;
        }

        private static string RemoveModelSuffix(string source, string suffix) =>
            source.Remove(source.Length - suffix.Length);
    }
}
