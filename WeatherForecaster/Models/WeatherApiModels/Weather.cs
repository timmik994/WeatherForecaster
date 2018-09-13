namespace WeatherForecaster.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Current weather API response.
    /// </summary>
    public class WeatherResponse
    {
        /// <summary>
        /// Gets or sets time and data when weather came in UNIX format.
        /// </summary>
        [JsonProperty("dt")]
        public long DateTime { get; set; }

        /// <summary>
        /// Gets or sets temperature.
        /// </summary>
        [JsonProperty("main")]
        public TemperatureRecord TemperatureRecord { get; set; }
    }
}
