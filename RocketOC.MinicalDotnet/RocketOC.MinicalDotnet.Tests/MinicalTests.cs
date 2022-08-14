using System;
using Xunit;

namespace RocketOC.MinicalDotnet.Tests
{
    public class MinicalTests
    {
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