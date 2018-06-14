using ReadingList.Domain.Infrastructure;

namespace ReadingList.Domain.Commands
{
    public class CommandResult : Result
    {
        
        public static CommandResult Succeed() => new CommandResult(true);

        public static CommandResult Failed(string errorMessage) =>
            new CommandResult(false, errorMessage);

        private CommandResult(bool isSucceed, string errorMessage = null) : base(isSucceed, errorMessage)
        {
        }
    }
    
    public class CommandResult<T> : Result<T>
    {
        
        public static CommandResult<T> Succeed(T data) => new CommandResult<T>(true, data);
        public static CommandResult<T> Failed(string errorMessage) => new CommandResult<T>(false, errorMessage);

        private CommandResult(bool isSucceed, T data) : base(isSucceed, data)
        {
        }

        private CommandResult(bool isSucceed, string errorMessage) : base(isSucceed, errorMessage)
        {
        }
    }
}