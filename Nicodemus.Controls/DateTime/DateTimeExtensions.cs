﻿using System;

namespace Nicodemus.Controls.Common
{
    internal static class DateTimeExtensions
    {
        public static DateTime SumWithoutOverflow(this DateTime value, TimeSpan offset)
        {
            if (offset > TimeSpan.Zero && value >= DateTime.MaxValue - offset) // overflow
                return DateTime.MaxValue;
            else if (offset < TimeSpan.Zero && value <= DateTime.MinValue - offset)
                return DateTime.MinValue;
            else
                return value + offset;
        }

        public static TimeSpan UtcOffset(this DateTime value)
        {
            return TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
        }

    }
}
