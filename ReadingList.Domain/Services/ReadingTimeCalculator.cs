using System;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Services
{
    public static class ReadingTimeCalculator
    {
        public static TimeSpan Calculate(BookItemStatus previousStatus, DateTime previousDate, BookItemStatus newStatus)
        {
            var diff = default(TimeSpan);
            if (previousStatus == newStatus)
                return diff;
            switch (newStatus)
            {
                case BookItemStatus.StartedButPostponed:
                case BookItemStatus.Read:
                    diff = DateTime.Now - previousDate;
                    break;
                case BookItemStatus.ToReading:
                    break;
                case BookItemStatus.Reading:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }

            return diff;
        }
    }
}