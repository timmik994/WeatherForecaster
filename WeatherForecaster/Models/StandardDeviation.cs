namespace WeatherForecaster.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Standard deviation for weather forecast.
    /// </summary>
    public class StandardDeviation
    {
        /// <summary>
        /// Gets or sets id of deviation.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets delta between time when forecast was made and forecasted weather came.
        /// </summary>
        public int ForecastMadeForecastCameTimeDelta { get; set; }

        /// <summary>
        /// Gets or sets standard deviation value.
        /// </summary>
        public float Deviation { get; set; }

        /// <summary>
        /// Gets or sets standard deviation calculation time.
        /// </summary>
        public DateTime CalculationTime { get; set; }
    }
}
