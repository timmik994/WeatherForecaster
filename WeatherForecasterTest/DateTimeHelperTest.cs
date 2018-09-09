namespace WheatherForecasterTest
{
    using System;
    using WheatherForecaster.Services;
    using Xunit;

    /// <summary>
    /// Tests for DateTimeHelper.
    /// </summary>
    public class DateTimeHelperTest
    {
        /// <summary>
        /// Tests calculation of delta between to dates in hours.
        /// </summary>
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

        /// <summary>
        /// Tests that returns right forecast time.
        /// </summary>
        /// <param name="delta">Delta between actual and forecast times.</param>
        /// <param name="expected">Expected value.</param>
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
