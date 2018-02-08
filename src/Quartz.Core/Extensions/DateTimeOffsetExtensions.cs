using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.Core
{
    public static class DateTimeOffsetExtensions
    {
        public static long UnixTicks(this DateTime dt)
        {
            var unixTimestampOrigin = new DateTime(1970, 1, 1);
            return (long)new TimeSpan(dt.Ticks - unixTimestampOrigin.Ticks).TotalMilliseconds;
        }
        public static DateTime? ToDateTime(this DateTimeOffset? offset)
        {
            if (offset.HasValue)
                return offset.Value.DateTime;
            return null;
        }

        public static string ToDateTimeString(this DateTimeOffset? offset)
        {
            if (offset.HasValue)
                return offset.Value.DateTime.ToString("yyyy-MM-dd");
            return string.Empty;
        }
    }
}
