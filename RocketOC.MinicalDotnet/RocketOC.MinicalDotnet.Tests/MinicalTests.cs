using System;
using System.Collections.Generic;
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
            var location = CalendarChunk.GetLocation(zeroDate, 0, zeroDate);

            //assert
            //roll August 2022 counter-clockwise to visualize. Sunday is 6 and Saturday is 0.
            Assert.Equal((5, 0), location);
        }

        [Fact]
        public void PrintActivity_MultipleRows_ShouldNotThrow()
        {
            var mini = new Minical();

            var commits = new List<DateOnly>(){
                new (2022, 07, 06),
                new (2022, 08, 02),
                new (2023, 08, 15),
            };

            //2 blocks of separation beween months
            mini.PrintActivity(commits, 2, true, 3);
        }
    }
}