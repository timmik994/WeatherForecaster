namespace WheatherForecaster.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Record of forecast whether and actual whether
    /// </summary>
    public class WeatherRecord
    {
        /// <summary>
        /// Gets or sets id of record in database.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this record has forecast and actual whether.
        /// </summary>
        public bool IsFull { get; set; }

        /// <summary>
        /// Gets or sets date and time when forecast was done in UTC format.
        /// </summary>
        public DateTime ForecastTime { get; set; }

        /// <summary>
        /// Gets or sets forecast temperature.
        /// </summary>
        public float ForecastTempreche { get; set; }

        /// <summary>
        /// Gets or sets hours forward of forecast.
        /// </summary>
        public int ForecastHours { get; set; }

        /// <summary>
        /// Gets or sets Actual temperature
        /// </summary>
        public float ActualTempreche { get; set; }
    }
}
