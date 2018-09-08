using System;
using System.ComponentModel.DataAnnotations;
using WheatherForecaster.Services;
using Xunit;

namespace WhetherForecasterTest
{
    public class DateTimeHelperTest
    {
        [Fact]
        public void GetDateDeltaInHoursTest()
        {
            // arrange
            int expectedDelta = 3;
            DateTime toDate = DateTime.Now.AddHours(expectedDelta);

            // act
            int delta = DateTimeHelper.GetDateDeltaInHours(DateTime.Now, toDate);

            // assert
            Assert.Equal(expectedDelta, delta);
        }

        [Theory]
        [InlineData(4, 3)]
        [InlineData(6, 6)]
        [InlineData(5, 6)]
        public void GetForecastHoursTest(int delta, int expected)
        {
            // act
            int forecastHours = DateTimeHelper.GetForecastHours(delta);

            // assert
            Assert.Equal(expected, forecastHours);
        }
    }
}
