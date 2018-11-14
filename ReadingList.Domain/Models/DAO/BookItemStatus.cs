using System.ComponentModel;

namespace ReadingList.Domain.Models.DAO
{
    public enum BookItemStatus
    {
        [Description("To Reading")]
        ToReading = 1,
        Reading,
        [Description("Started But Postponed")]
        StartedButPostponed,
        Read
    }
}