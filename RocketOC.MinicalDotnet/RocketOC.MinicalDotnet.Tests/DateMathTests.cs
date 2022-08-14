using System;
using Xunit;

namespace RocketOC.MinicalDotnet.Tests
{
    public class DateMathTests
    {
        [Theory]
        [InlineData(2022, 8, 2022, 8, 0)]
        [InlineData(2022, 8, 2022, 9, 1)]
        [InlineData(2022, 8, 2023, 8, 12)]
        [InlineData(2022, 8, 2023, 9, 13)]
        public void MonthsBetween_ValidInput_ValidOutput(int startYear, int startMonth, int endYear, int endMonth, int expectedMonths)
        {
            //arrange
            var startDate = new DateOnly(startYear, startMonth, 1);
            var endDate = new DateOnly(endYear, endMonth, 1);

            //act
            var months = DateMath.MonthsBetween(startDate, endDate);

            //assert
            Assert.Equal(expectedMonths, months);
        }

        [Fact]
        public void MonthsBetween_OutOfOrderDates_Throws()
        {
            //arrange
            var startDate = new DateOnly(2022, 8, 1);
            var endDate = new DateOnly(2022, 7, 31);

            //act and assert
            Assert.Throws<ArgumentException>(() => DateMath.MonthsBetween(startDate, endDate));
        }

        [Fact]
        public void IsOnOrBeforeIgnoringDay_EarlierHasSameMonthAndLaterDay_IndicatesBefore()
        {
            //notice that "earlierDate" is actually chronologically after "laterDate"
            //the variables are named according their argument usage

            //arrange
            var earlierDate = new DateOnly(2022, 8, 15);
            var laterDate = new DateOnly(2022, 8, 6);

            //assert
            Assert.True(earlierDate.IsOnOrBeforeIgnoringDay(laterDate));
        }

        [Fact]
        public void IsOnOrBeforeIgnoringDay_EarlierHasEarlierMonthAndSameYear_IndicatesBefore()
        {
            //arrange
            var earlierDate = new DateOnly(2022, 4, 15);
            var laterDate = new DateOnly(2022, 8, 6);

            //assert
            Assert.True(earlierDate.IsOnOrBeforeIgnoringDay(laterDate));
        }

        [Fact]
        public void IsOnOrBeforeIgnoringDay_EarlierHasLaterMonthAndEarlierYear_IndicatesBefore()
        {
            //arrange
            var earlierDate = new DateOnly(2021, 9, 15);
            var laterDate = new DateOnly(2022, 8, 6);

            //assert
            Assert.True(earlierDate.IsOnOrBeforeIgnoringDay(laterDate));
        }

        [Fact]
        public void IsOnOrBeforeIgnoringDay_EarlierHasLaterMonthAndSameYear_IndicatesAfter()
        {
            //arrange
            var earlierDate = new DateOnly(2022, 9, 15);
            var laterDate = new DateOnly(2022, 8, 6);

            //assert
            Assert.False(earlierDate.IsOnOrBeforeIgnoringDay(laterDate));
        }
    }
}
