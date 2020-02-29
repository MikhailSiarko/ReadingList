namespace ReadingList.Domain.Infrastructure
{
    public class SelectListItem<T>
    {
        public string Text { get; set; }

        public T Value { get; set; }
    }
}