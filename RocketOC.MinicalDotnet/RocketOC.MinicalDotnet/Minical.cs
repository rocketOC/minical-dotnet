namespace RocketOC.MinicalDotnet
{
    public class Minical
    {
        /// <summary>
        /// Write to the console a mini calendar of activity.
        /// </summary>
        /// <param name="dates">IEnumerable of dates when activity happened. Must contain atleast 1 date.</param>
        /// <param name="sep">Horizontal separation between months. 0 and 2 are nice.</param>
        /// <param name="labelMonths">Add labeling to the calendar.</param>
        /// <exception cref="ArgumentException"></exception>
        public void PrintActivity(IEnumerable<DateOnly> dates, int sep, bool labelMonths)
        {
            if (!dates.Any())
                throw new ArgumentException($"{nameof(dates)} needs at least one date");

            var firstDate = dates.Min();
            var lastDate = dates.Max();
            var zeroDate = new DateOnly(firstDate.Year, firstDate.Month, 1);

            int months = DateMath.MonthsBetween(zeroDate, lastDate) + 1; //how many months should we display?

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
            while (zeroDate.AddDays(i).IsOnOrBeforeIgnoringDay(lastDate))
            {
                var location = GetLocation(zeroDate, sep, zeroDate.AddDays(i));
                calendar[location.Item1, location.Item2] = true;
                i++;
            }

            //get the month labels
            string[]? labels = null;
            if (labelMonths)
            {
                labels = new string[months * (5 + sep) + 2];
                i = 0;
                while (zeroDate.AddMonths(i).IsOnOrBeforeIgnoringDay(lastDate))
                {
                    var location = GetLocation(zeroDate, sep, zeroDate.AddMonths(i));
                    //let the user pass the format string?
                    labels[location.Item2] = zeroDate.AddMonths(i).ToString("MMM `yy");
                    i++;
                }
            }

            PrintActivity(calendar, activity, labels);
        }

        /// <summary>
        /// Calendar is a bool [,] indicating the positions that represent days rather than white space or padding.
        /// Activity is a bool [,]. We will put an x where activity is true. The activity array should use the same
        /// date logic as the calendar.
        /// </summary>
        internal virtual void PrintActivity(bool[,] calendar, bool[,] activity, string[]? monthLabels)
        {
            //print labels
            if (monthLabels != null)
            {
                var labelRow = new char[4 * calendar.GetLength(1)];
                for (int i = 0; i < labelRow.Length; i++)
                {
                    labelRow[i] = ' ';
                }
                for (int i = 0; i < monthLabels.Length; i++)
                {
                    if (monthLabels[i] != null)
                    {
                        //4 here comes from the block width used below
                        int display = Math.Min(5 * 4, monthLabels[i].Length);
                        for (int j = 0; j < display; j++)
                        {
                            labelRow[i*4 + j] = monthLabels[i][j];
                        }
                    }
                }
                Console.WriteLine(new string(labelRow));
            }

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
            int col = (dayOfYear + firstOfTheCalendarDay) / 7 + separation * DateMath.MonthsBetween(zeroDate, someDate);
            return (row, col);
        }
    }
}