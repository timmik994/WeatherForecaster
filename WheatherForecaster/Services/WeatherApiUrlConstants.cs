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
        public const string Forecast = "http://api.openweathermap.org/data/2.5/forecast?q=Kazan&units=metric&APPID=db6a0275a5617f829885a767e5d2a9dd";

        /// <summary>
        /// URL to get actual weather.
        /// </summary>
        public const string ActualWeather = "http://api.openweathermap.org/data/2.5/weather?q=Kazan&units=metric&APPID=db6a0275a5617f829885a767e5d2a9dd";
    }
}
