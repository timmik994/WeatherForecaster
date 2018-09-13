namespace WeatherForecaster.Models
{
    using System;

    /// <summary>
    /// Data model of forecasted weather.
    /// </summary>
    public class WeatherViewModel
    {
        /// <summary>
        /// Gets or sets max temperature considering standard deviation.
        /// </summary>
        public float MaxTemperature { get; set; }

        /// <summary>
        /// Gets or sets actual temperature in forecast.
        /// </summary>
        public float ActualTemperature { get; set; }

        /// <summary>
        /// Gets or sets min temperature considering standard deviation.
        /// </summary>
        public float MinTemperature { get; set; }

        /// <summary>
        /// Gets or sets time when forecasted weather came.
        /// </summary>
        public DateTime ForecastTime { get; set; }
    }
}
