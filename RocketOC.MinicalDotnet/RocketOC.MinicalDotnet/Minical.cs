namespace RocketOC.MinicalDotnet
{
    public class Minical
    {
        /// <summary>
        /// Write to the console a mini calendar of activity.
        /// </summary>
        /// <param name="dates">IEnumerable of dates when activity happened. Must contain atleast 1 date.</param>
        /// <param name="sep">Horizontal separation between months. 0 and 2 are nice.</param>
        /// <exception cref="ArgumentException"></exception>
        public void PrintActivity(IEnumerable<DateOnly> dates, int sep)
        {
            if (!dates.Any())
                throw new ArgumentException($"{nameof(dates)} needs at least one date");

            var firstDate = dates.Min();
            var lastDate = dates.Max();
            var zeroDate = new DateOnly(firstDate.Year, firstDate.Month, 1);

            int months = MonthsBetween(zeroDate, lastDate) + 1; //how many months should we display?

            //todo: try to remove any extra right padding
            //make the array of the activity data
            var activity = new bool[7, months * (5 + sep) + 2]; //5 cols of dates per month + sep padding
            foreach (var activeDay in dates)
            {
                var location = GetLocation(zeroDate, sep, activeDay);
                activity[location.Item1, location.Item2] = true;
            }

            //make the array that indicates which cells are white space and which are dates
            var calendar = new bool[7, months * (5 + sep) + 2];
            int i = 0;
            while ((zeroDate.AddDays(i).Year < lastDate.Year) || (zeroDate.AddDays(i).Year == lastDate.Year && zeroDate.AddDays(i).Month <= lastDate.Month))
            {
                var location = GetLocation(zeroDate, sep, zeroDate.AddDays(i));
                calendar[location.Item1, location.Item2] = true;
                i++;
            }

            PrintActivity(calendar, activity);
        }

        /// <summary>
        /// Calendar is a bool [,] indicating the positions that represent days rather than white space or padding.
        /// Activity is a bool [,]. We will put an x where activity is true. The activity array should use the same
        /// date logic as the calendar.
        /// </summary>
        internal virtual void PrintActivity(bool[,] calendar, bool[,] activity)
        {
            for (int r = 0; r < calendar.GetLength(0); r++)
            {
                for (int c = 0; c < calendar.GetLength(1); c++)
                {
                    if (calendar[r, c] || r > 0 && calendar[r - 1, c])
                        Console.Write("+―――");
                    else if (c > 0 && calendar[r, c - 1])
                        Console.Write("+   ");
                    else
                        Console.Write("    ");
                }
                Console.WriteLine();

                for (int c = 0; c < calendar.GetLength(1); c++)
                {
                    if (calendar[r, c] && activity[r, c])
                    {
                        Console.Write("| X ");
                    }
                    else if (calendar[r, c])
                    {
                        Console.Write("|   ");
                    }
                    else if (c > 0 && calendar[r, c - 1])
                    {
                        Console.Write("|   ");
                    }
                    else
                    {
                        Console.Write("    ");
                    }
                }
                Console.WriteLine();
            }
            //one last row of +-+
            var height = calendar.GetLength(0);
            for (int c = 0; c < calendar.GetLength(1); c++)
            {
                if (calendar[height - 1, c])
                    Console.Write("+―――");
                else if (c > 0 && calendar[height - 1, c - 1])
                    Console.Write("+   ");
                else
                    Console.Write("    ");
            }
            Console.WriteLine();
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
            int col = (dayOfYear + firstOfTheCalendarDay) / 7 + separation * MonthsBetween(zeroDate, someDate);
            return (row, col);
        }

        /// <summary>
        /// Find the number of months between two dates.
        /// </summary>
        internal static int MonthsBetween(DateOnly startDate, DateOnly endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException($"{nameof(endDate)} should be on or after {nameof(startDate)}");
            }

            int months = 0;
            if (startDate.Year == endDate.Year)
            {
                months = endDate.Month - startDate.Month;
            }
            else
            {
                if (startDate.Month <= endDate.Month)
                {
                    months = 12 * (endDate.Year - startDate.Year) + (endDate.Month - startDate.Month);
                }
                else
                {
                    months = 12 * (endDate.Year - startDate.Year - 1) + (12 - startDate.Month) + endDate.Month;
                }
            }
            return months;
        }
    }
}