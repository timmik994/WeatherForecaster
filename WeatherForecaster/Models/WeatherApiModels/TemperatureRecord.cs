namespace WeatherForecaster.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Record of the temperature.
    /// </summary>
    public class TemperatureRecord
    {
        /// <summary>
        /// Gets or sets temperature.
        /// </summary>
        [JsonProperty("temp")]
        public float Temperature { get; set; }
    }
}
