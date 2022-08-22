namespace RocketOC.MinicalDotnet
{
    internal class CalendarChunk
    {
        internal bool[,] Activity { get; set; }
        internal bool[,] Days { get; set; }
        internal string[]? Labels { get; set; }

        internal CalendarChunk(IEnumerable<DateOnly> dates, int sep, bool labelMonths, DateOnly zeroDate, DateOnly lastDate)
        {
            var months = DateMath.MonthsBetween(zeroDate, lastDate) + 1;
            var width = months * (5 + sep) + 2;

            //todo: try to remove any extra right padding
            Activity = new bool[7, width];
            foreach (var activeDay in dates.Where(d => d >= zeroDate && d <= lastDate))
            {
                var location = GetLocation(zeroDate, sep, activeDay);
                Activity[location.Item1, location.Item2] = true;
            }

            //make the array that indicates which cells are white space and which are dates
            Days = new bool[7, width];
            int i = 0;
            while (zeroDate.AddDays(i).IsOnOrBeforeIgnoringDay(lastDate))
            {
                var location = GetLocation(zeroDate, sep, zeroDate.AddDays(i));
                Days[location.Item1, location.Item2] = true;
                i++;
            }

            //get the month labels
            Labels = null;
            if (labelMonths)
            {
                Labels = new string[width];
                i = 0;
                while (zeroDate.AddMonths(i).IsOnOrBeforeIgnoringDay(lastDate))
                {
                    var location = GetLocation(zeroDate, sep, zeroDate.AddMonths(i));
                    //let the user pass the format string?
                    Labels[location.Item2] = zeroDate.AddMonths(i).ToString("MMM `yy");
                    i++;
                }
            }
        }

        /// <summary>
        /// Get the location for a date in a two dimensional coordinate system where days of week are vertical and weeks are horizontal.
        /// Sunday on the bottom.
        /// </summary>
        /// <param name="zeroDate">The earliest date the calendar needs to represent.</param>
        /// <param name="separation">Number of spaces between months. 0 and 2 are nice.</param>
        /// <param name="someDate">Date to locate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        internal static (int, int) GetLocation(DateOnly zeroDate, int separation, DateOnly someDate)
        {
            if (zeroDate.Day != 1)
            {
                throw new ArgumentException($"{nameof(zeroDate)} must be on the first of the month");
            }
            if (separation < 0)
            {
                throw new ArgumentException($"{nameof(separation)} must be >= 0");
            }
            //todo: someDate should be >= zeroDate

            int firstOfTheCalendarDay = (int)(zeroDate).DayOfWeek;
            int dayOfYear = (someDate.ToDateTime(new TimeOnly(0, 0, 0)) - zeroDate.ToDateTime(new TimeOnly(0, 0, 0))).Days;
            int dayOfWeek = (int)someDate.DayOfWeek; //sunday is 0
            int row = 6 - dayOfWeek;
            int col = (dayOfYear + firstOfTheCalendarDay) / 7 + separation * DateMath.MonthsBetween(zeroDate, someDate);
            return (row, col);
        }
    }
}
