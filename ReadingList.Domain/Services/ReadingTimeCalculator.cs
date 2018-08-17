using System;
using ReadingList.WriteModel.Models;

namespace ReadingList.Domain.Services
{
    public static class ReadingTimeCalculator
    {
        public static int Calculate(BookItemStatus previousStatus, DateTime previousDate, BookItemStatus newStatus)
        {
            var diff = default(int);
            if (previousStatus == newStatus)
                return diff;
            switch (newStatus)
            {
                case BookItemStatus.StartedButPostponed:
                case BookItemStatus.Read:
                    diff = Convert.ToInt32(Math.Round((DateTime.Now - previousDate).TotalSeconds, MidpointRounding.AwayFromZero));
                    break;
                case BookItemStatus.ToReading:
                case BookItemStatus.Reading:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }

            return diff;
        }
    }
}