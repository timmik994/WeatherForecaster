using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatherForecaster.Services
{
    /// <summary>
    /// Urls to weather API.
    /// </summary>
    public class WeatherApiUrlConstants
    {
        /// <summary>
        /// URL to get forecasts.
        /// </summary>
        public const string Forecast = "api.openweathermap.org/data/2.5/forecast?q=Kazan,us&units=celsium";

        /// <summary>
        /// URL to get actual weather.
        /// </summary>
        public const string ActualWeather = "api.openweathermap.org/data/2.5/weather?q=Kazan,us&units=celsium";
    }
}
