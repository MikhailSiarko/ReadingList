namespace ReadingList.Domain.Commands
{
    public class CommandResult
    {
        public bool IsSucceed { get; }
        public string ErrorMessage { get; }
        
        public static CommandResult Successed() => new CommandResult(true);

        public static CommandResult Failed(string errorMessage) =>
            new CommandResult(false, errorMessage);

        public CommandResult(bool isSucceed)
        {
            IsSucceed = isSucceed;
        }
        
        public CommandResult(bool isSucceed, string errorMessage) : this(isSucceed)
        {
            ErrorMessage = errorMessage;
        }
    }
}