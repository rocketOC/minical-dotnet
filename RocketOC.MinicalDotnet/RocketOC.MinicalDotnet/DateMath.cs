namespace RocketOC.MinicalDotnet
{
    internal static class DateMath
    {
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

        /// <summary>
        /// Is earlierDate actually earlier than laterDate when ignoring the day of the month?
        /// </summary>
        /// <param name="earlierDate"></param>
        /// <param name="laterDate"></param>
        /// <returns></returns>
        internal static bool IsOnOrBeforeIgnoringDay(this DateOnly earlierDate, DateOnly laterDate)
        {
            return (earlierDate.Year < laterDate.Year) 
                || (earlierDate.Year == laterDate.Year && earlierDate.Month <= laterDate.Month);
        }
    }
}
