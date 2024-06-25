using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NTools.SchedulerService
{
    internal class Schedule
    {
        private static readonly char[] Separators = { ' ' };
        
        public static Schedule Parse(string Schedule) 
        {
            Debug.Assert(Schedule != null);

            var fields = Schedule.Split((char[])Separators, StringSplitOptions.RemoveEmptyEntries);

            if (fields.Length != 4)
            {
                throw new FormatException(string.Format(
                    "'{0}' is not a valid crontab expression. It must contain at least 4 components of a schedule "
                    + "(in the sequence of minutes, hours, days, months).",
                    Schedule));
            }

            return new Schedule(int.Parse(fields[0]), int.Parse(fields[1]),int.Parse(fields[2]),int.Parse(fields[3]));
        }
        public Schedule(int minute, int hour, int day, int month)
        {
            Minute = minute;
            Hour = hour;
            Day = day;
            Month = month;
        }

        public int Minute { get; }
        public int Hour { get; }
        public int Day { get; }
        public int Month { get; }

        public DateTime GetNextOccurrence(DateTime baseTime)
        {
            return baseTime.AddMonths(Month).AddDays(Day).AddHours(Hour).AddMinutes(Minute);
        }
    }
}
