using System;
using ReadingList.Models.Write;

namespace ReadingList.Domain.Infrastructure
{
    public static class ReadingTimeCalculator
    {
        public static int Calculate(int previousTime, BookItemStatus previousStatus, DateTimeOffset previousDate,
            BookItemStatus newStatus)
        {
            var diff = default(int);
            if (previousStatus == newStatus)
                return previousTime + diff;
            switch (newStatus)
            {
                case BookItemStatus.StartedButPostponed:
                case BookItemStatus.Read:
                    diff = Convert.ToInt32(Math.Round((DateTime.UtcNow - previousDate).TotalSeconds,
                        MidpointRounding.AwayFromZero));
                    break;
                case BookItemStatus.ToReading:
                case BookItemStatus.Reading:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }

            return previousTime + diff;
        }
    }
}