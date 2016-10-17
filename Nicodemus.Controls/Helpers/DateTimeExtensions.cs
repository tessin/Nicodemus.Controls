using System;

namespace Nicodemus.Controls.Helpers
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
    }
}
