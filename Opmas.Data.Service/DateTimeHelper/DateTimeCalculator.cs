﻿using System;

namespace Opmas.Data.Service.DateTimeHelper
{
    public class DateTimeCalculator
    {
        /// <summary>
        /// This mehod calculates the time a news was posted form the present time
        /// </summary>
        /// <param name="dateTimeValue"></param>
        /// <returns></returns>
        public string TimeAgo(DateTime dateTimeValue)
        {
            var span = DateTime.Now - dateTimeValue;
            if (span.Days > 365)
            {
                var years = span.Days/365;
                if (span.Days%365 != 0)
                    years += 1;
                return $"about {years} {(years == 1 ? "year" : "years")} ago";
            }
            if (span.Days > 30)
            {
                var months = span.Days/30;
                if (span.Days%31 != 0)
                    months += 1;
                return $"about {months} {(months == 1 ? "month" : "months")} ago";
            }
            if (span.Days > 0)
                return $"about {span.Days} {(span.Days == 1 ? "day" : "days")} ago";
            if (span.Hours > 0)
                return $"about {span.Hours} {(span.Hours == 1 ? "hour" : "hours")} ago";
            if (span.Minutes > 0)
                return $"about {span.Minutes} {(span.Minutes == 1 ? "minute" : "minutes")} ago";
            if (span.Seconds > 5)
                return $"about {span.Seconds} seconds ago";
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
}