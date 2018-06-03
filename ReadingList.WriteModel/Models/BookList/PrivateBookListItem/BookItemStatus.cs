using System.ComponentModel;

namespace ReadingList.WriteModel.Models
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