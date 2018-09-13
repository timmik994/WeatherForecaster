namespace WeatherForecaster.Services
{
    using System;

    /// <summary>
    /// Helper to work with DateTime.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Zero point when UNIX starts calculate time.
        /// </summary>
        private static DateTime unixZeroPoint = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Gets rounded delta in hours between two dates.
        /// </summary>
        /// <param name="fromDateTime">Less date.</param>
        /// <param name="toDateTime">Higher date.</param>
        /// <returns>Delta between dates in hours.</returns>
        public static int GetDeltaInHours(DateTime fromDateTime, DateTime toDateTime)
        {
            TimeSpan delta = toDateTime - fromDateTime;
            double doubleDeltaInHours = delta.TotalHours;  
            int roundedDeltaInHours = (int)Math.Round(doubleDeltaInHours);
            return roundedDeltaInHours;
        }

        /// <summary>
        /// Leads delta in hours to value divisible by forecast step size.
        /// </summary>
        /// <param name="deltaHours">Forecast time in hours.</param>
        /// <returns>Forecast time in hours divided by forecast step.</returns>
        public static int LeadDeltaToValueDivisibleByStep(int deltaHours)
        {
            int deltaRemainder = deltaHours % ForecastConstants.ForecastTimeStep;
            int deltaRemainderLimit = ForecastConstants.ForecastTimeStep / 2;
            if (deltaRemainder <= deltaRemainderLimit)
            {
                deltaHours = deltaHours - deltaRemainder;
            }
            else
            {
                deltaHours = deltaHours - deltaRemainder + ForecastConstants.ForecastTimeStep;
            }

            return Math.Abs(deltaHours);
        }

        /// <summary>
        /// Converts UNIX time stamp to DateTime.
        /// </summary>
        /// <param name="unixTimeStamp">UNIX time stamp</param>
        /// <returns>Actual dateTime.</returns>
        public static DateTime FromUnixUTCTime(long unixTimeStamp)
        {
            DateTime result = DateTimeHelper.unixZeroPoint.AddSeconds(unixTimeStamp).ToLocalTime();
            return result;
        }
    }
}
