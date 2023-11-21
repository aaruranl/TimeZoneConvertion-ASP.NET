using Microsoft.AspNetCore.Mvc;
using System;

namespace timezone.Services
{
    public class TimeZoneConversion
    {
        public DateTime ToUctTime(DateTime dateTime, string timeZoneId) 
        {
            TimeZoneInfo convertTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            //TimeZoneInfo convertTimeZone = TimeZoneInfo.Local;
            DateTimeOffset targetDateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(targetDateTimeOffset.DateTime, convertTimeZone);
            return utcDateTime;
        }


        public DateTime ToOutTime(DateTime dateTime, string timeZoneId)
        {
            TimeZoneInfo convertTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            //TimeZoneInfo convertTimeZone = TimeZoneInfo.Local;
            DateTimeOffset storedDateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
            DateTimeOffset targetDateTimeOffset = TimeZoneInfo.ConvertTime(storedDateTimeOffset, convertTimeZone);
            DateTime outTime = targetDateTimeOffset.DateTime;
            return outTime;
        }
    }
}
