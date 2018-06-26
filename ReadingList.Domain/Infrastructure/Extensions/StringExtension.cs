namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string F(this string source, params object[] args) => string.Format(source, args);
    }
}
