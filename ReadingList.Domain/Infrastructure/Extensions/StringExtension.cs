namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string F(this string source, params object[] args) => string.Format(source, args);

        public static string RemoveWithData(this string source)
        {
            return source.Replace(" with {0}", string.Empty);
        }

        public static string RemoveWith(this string source)
        {
            return source.Replace(" with ", string.Empty);
        }
    }
}