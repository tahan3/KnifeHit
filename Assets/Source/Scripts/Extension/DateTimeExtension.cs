using System;

namespace Source.Extension
{
    public static class DateTimeExtension
    {
        public static DateTime Trim(this DateTime date, long ticks) {
            return new DateTime(date.Ticks - (date.Ticks % ticks), date.Kind);
        }
    }
}