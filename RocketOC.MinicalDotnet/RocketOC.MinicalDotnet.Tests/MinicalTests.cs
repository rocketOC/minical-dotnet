using System;
using Xunit;

namespace RocketOC.MinicalDotnet.Tests
{
    public class MinicalTests
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
            var months = Minical.MonthsBetween(startDate, endDate);

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
            Assert.Throws<ArgumentException>(() => Minical.MonthsBetween(startDate, endDate));
        }

        [Fact]
        public void GetLocation_ZeroDateEqualsSomeDate_ReturnCorrectLocation()
        {
            //I want to make many of these tests, but I don't know what naming convention to use

            //arrange
            var zeroDate = new DateOnly(2022, 8, 1);

            //act
            var location = Minical.GetLocation(zeroDate, 0, zeroDate);

            //assert
            //roll August 2022 counter-clockwise to visualize. Sunday is 6 and Saturday is 0.
            Assert.Equal((5, 0), location);
        }
    }
}