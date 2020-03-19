using System;

namespace Demo.Util
{
    public static class DateTimeExtension
    {
        public static DateTime? ConvertToUtcNull(this DateTime? source, bool isDevelopment)
        {
            if (source == null)
            {
                return null;
            }

            return ConvertToUtc(source.Value, isDevelopment);
        }

        public static DateTime? ConvertToLocalNull(this DateTime? source, bool isDevelopment)
        {
            if (source == null)
            {
                return null;
            }

            return ConvertToLocal(source.Value, isDevelopment);
        }

        public static DateTime ConvertToUtc(this DateTime source, bool isDevelopment)
        {
            if (source.Kind == DateTimeKind.Utc)
            {
                return source;
            }

            var userDateTime = source.Kind == DateTimeKind.Local ? DateTime.SpecifyKind(source, DateTimeKind.Unspecified) : source;

            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(userDateTime, isDevelopment ? Constants.DefaultTimeZone : Constants.DefaultTimeZoneDeploy, TimeZoneInfo.Utc.Id);
        }

        public static DateTime ConvertToLocal(this DateTime source, bool isDevelopment)
        {
            var userDateTime = DateTime.SpecifyKind(source, DateTimeKind.Utc);
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(isDevelopment ? Constants.DefaultTimeZone : Constants.DefaultTimeZoneDeploy);

            return TimeZoneInfo.ConvertTimeFromUtc(userDateTime, userTimeZone);
        }


        public static DateTime ToStartDateTime(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, item.Day);
        }

        public static DateTime ToEndDateTime(this DateTime item)
        {
            return new DateTime(item.Year, item.Month, item.Day, 23, 59, 59);
        }

        public static string ToShortDateTimeString(this DateTime dateTime)
        {
            return ToString(dateTime, Constants.DefaultShortDateTimeFormat);
        }

        public static string ToShortDateTimeNullString(this DateTime? dateTime)
        {
            return ToNullString(dateTime, Constants.DefaultShortDateTimeFormat);
        }

        public static string ToDateTimeString(this DateTime dateTime)
        {
            return ToString(dateTime, Constants.DefaultShortDateTimeFormat);
        }

        public static string ToDateTimeNullString(this DateTime? dateTime)
        {
            return ToNullString(dateTime, Constants.DefaultShortDateTimeFormat);
        }

        public static string ToDateString(this DateTime dateTime)
        {
            return ToString(dateTime, Constants.DefaultDateFormat);
        }

        public static string ToDateNullString(this DateTime? dateTime)
        {
            return ToNullString(dateTime, Constants.DefaultDateFormat);
        }

        public static string ToTimeString(this DateTime dateTime)
        {
            return ToString(dateTime, Constants.DefaultTimeFormat);
        }

        public static string ToTimeNullString(this DateTime? dateTime)
        {
            return ToNullString(dateTime, Constants.DefaultTimeFormat);
        }

        public static string ToRelativeTime(this DateTime dateTime)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "1 giây trước" : ts.Seconds + " giây trước";

            if (delta < 2 * MINUTE)
                return "1 phút trước";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút trước";

            if (delta < 90 * MINUTE)
                return "1 giờ trước";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 48 * HOUR)
                return "hôm qua";

            if (delta < 30 * DAY)
                return ts.Days + " ngày trước";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "1 tháng trước" : months + " tháng trước";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "1 năm trước" : years + " năm trước";
            }
        }

        private static string ToString(DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }

        private static string ToNullString(DateTime? dateTime, string format)
        {
            var date = dateTime.GetValueOrDefault();
            if (date != DateTime.MinValue)
            {
                return date.ToString(format);
            }
            return string.Empty;
        }
    }
}