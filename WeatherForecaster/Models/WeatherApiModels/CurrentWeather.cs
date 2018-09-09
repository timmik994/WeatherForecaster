namespace WheatherForecaster.Models
{
    using Newtonsoft.Json;
    using WheatherForecaster.Models.WheatherApiModels;

    /// <summary>
    /// Current whether API response.
    /// </summary>
    public class CurrentWeather
    {
        /// <summary>
        /// Gets or sets time of the forecast.
        /// </summary>
        [JsonProperty("dt")]
        public long DateTime { get; set; }

        /// <summary>
        /// Gets or sets temperature.
        /// </summary>
        [JsonProperty("main")]
        public TempechureRecord Tempreachure { get; set; }
    }
}
