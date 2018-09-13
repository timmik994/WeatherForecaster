namespace WeatherForecaster.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Record of forecasted weather and real weather
    /// </summary>
    public class WeatherRecord
    {
        /// <summary>
        /// Gets or sets id of record in database.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this record has forecasted and real weather.
        /// </summary>
        public bool IsFull { get; set; }

        /// <summary>
        /// Gets or sets date and time when forecasted weather came.
        /// </summary>
        public DateTime ForecastTime { get; set; }

        /// <summary>
        /// Gets or sets forecasted temperature.
        /// </summary>
        public float ForecastTemperature { get; set; }

        /// <summary>
        /// Gets or sets delta between time when forecast was made and forecasted weather came.
        /// </summary>
        public int ForecastMadeForecastCameTimeDelta { get; set; }

        /// <summary>
        /// Gets or sets Actual temperature
        /// </summary>
        public float ActualTemperature { get; set; }
    }
}
