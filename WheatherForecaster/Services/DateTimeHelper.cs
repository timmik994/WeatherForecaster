using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatherForecaster.Services
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Seconds in one hour.
        /// </summary>
        private const long SecondsInHour = 36000000000;

        /// <summary>
        /// Time in hours between forecasts.
        /// </summary>
        private const int ForecastStep = 3;

        /// <summary>
        /// Zero point when UNIX starts calculate time.
        /// </summary>
        private static DateTime UnitZeroPoint = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Gets delta in hours of firstUtcDate and secondUtcDate.
        /// </summary>
        /// <param name="firstUtcDate">Date in UTC format.</param>
        /// <param name="secondUtcDate">Date in UTC format.</param>
        /// <returns>Delta between dates in hours.</returns>
        public static int GetDateDeltaInHours(DateTime fromUtcDate, DateTime toUtcDate)
        {
            long secondsDelta = toUtcDate.ToFileTime() - fromUtcDate.ToFileTime();
            float hoursDelta = (float) secondsDelta / (float) DateTimeHelper.SecondsInHour;
            int trunkedDelta = (int) MathF.Round(hoursDelta);
            return trunkedDelta;
        }

        /// <summary>
        /// Calculates forecast time divided on forecast step. 
        /// </summary>
        /// <param name="deltaHours">Forecast time in hours.</param>
        /// <returns>Forecast time in hours divided by forecast step.</returns>
        public static int GetForecastHours(int deltaHours)
        {
            int deltaRemainder = deltaHours % DateTimeHelper.ForecastStep;
            int deltaRemainderLimit = DateTimeHelper.ForecastStep / 2;
            if (deltaRemainder <= deltaRemainderLimit)
            {
                deltaHours = deltaHours - deltaRemainder;
            }
            else
            {
                deltaHours = deltaHours - deltaRemainder + DateTimeHelper.ForecastStep;
            }

            return Math.Abs(deltaHours);
        }

        /// <summary>
        /// Converts UNIX time stamp to DateTime.
        /// </summary>
        /// <param name="UnixTimeStamp">UNIX time stamp</param>
        /// <returns>Actual dateTime.</returns>
        public static DateTime FromUnixUTCTime(long unixTimeStamp)
        {
            DateTime result = DateTimeHelper.UnitZeroPoint.AddSeconds(unixTimeStamp).ToLocalTime();
            return result;
        }
    }
}
