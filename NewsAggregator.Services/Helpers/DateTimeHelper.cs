using System;

namespace NewsAggregator.Services.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime AbsoluteStart(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime AbsoluteEnd(this DateTime dateTime)
        {
            return AbsoluteStart(dateTime).AddDays(1).AddMilliseconds(-1);
        }
    }
}
