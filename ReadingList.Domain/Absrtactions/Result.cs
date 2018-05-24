namespace ReadingList.Domain.Absrtactions
{
    public abstract class Result
    {
        public bool IsSucceed { get; set; }
        public string ErrorMessage { get; set; }

        protected Result(bool isSucceed, string errorMessage = null)
        {
            IsSucceed = isSucceed;
            ErrorMessage = errorMessage;
        }
    }

    public abstract class Result<T> : Result
    {
        public T Data { get; set; }

        protected Result(bool isSucceed, string errorMessage = null) : base(isSucceed, errorMessage)
        {
        }
        
        protected Result(bool isSucceed, T data, string errorMessage = null) : base(isSucceed, errorMessage)
        {
            Data = data;
        }
    }
}