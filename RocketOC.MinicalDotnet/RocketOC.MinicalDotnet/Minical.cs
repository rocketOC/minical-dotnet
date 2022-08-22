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
        /// <param name="wrapXMonths">Break afetr x months. -1 means never.</param>
        /// <exception cref="ArgumentException"></exception>
        public void PrintActivity(IEnumerable<DateOnly> dates, int sep, bool labelMonths, int wrapXMonths)
        {
            if (!dates.Any())
                throw new ArgumentException($"{nameof(dates)} needs at least one date");
            if (wrapXMonths == 0 || wrapXMonths < -1)
                throw new ArgumentException($"{nameof(wrapXMonths)} should be -1 or >= 1");

            var firstDate = dates.Min();
            var lastDate = dates.Max();
            var zeroDate = new DateOnly(firstDate.Year, firstDate.Month, 1);

            //how many months total are we going to display total? //hmmm
            int months = DateMath.MonthsBetween(zeroDate, lastDate) + 1;

            if(wrapXMonths == -1)
                wrapXMonths = months; //no wrapping (-1) is the same as wrapping by all

            //how many "rows" of months will we have
            var numRows = Math.Ceiling((1.0 * months) / wrapXMonths);

            for (int row = 0; row < numRows; row++)
            {
                var rowZeroDate = zeroDate.AddMonths(wrapXMonths * row);
                var lastDateRow = rowZeroDate.AddMonths(wrapXMonths).AddDays(-1);
                if (row == numRows - 1)
                    lastDateRow = lastDate;
                
                var chunk = new CalendarChunk(dates, sep, labelMonths, rowZeroDate, lastDateRow);

                ChunkPrinter.PrintChunk(chunk);

                if (row != numRows - 1)
                    Console.Write("\n\n");
            }
        }
    }
}