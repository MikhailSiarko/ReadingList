using System.ComponentModel;

namespace ReadingList.Domain.Enumerations
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