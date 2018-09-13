namespace WeatherForecaster.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Weather forecast response from API.
    /// </summary>
    public class WeatherForecastResponse
    {
        /// <summary>
        /// Gets or sets city where forecast was done.
        /// </summary>
        [JsonProperty("city")]
        public City City { get; set; }

        /// <summary>
        /// Gets or sets collection of weather records.
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<WeatherResponse> WeatherData { get; set; }
    }
}
