using System.Threading.Tasks;

namespace ReadingList.Application.Infrastructure.Extensions
{
    public static class TaskExtension
    {
        public static void RunSync(this Task task) => task.ConfigureAwait(false).GetAwaiter().GetResult();
        public static T RunSync<T>(this Task<T> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();
    }
}
