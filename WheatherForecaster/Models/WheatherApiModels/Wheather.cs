namespace WheatherForecaster.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using WheatherForecaster.Models.WheatherApiModels;

    /// <summary>
    /// Whether record from API.
    /// </summary>
    public class Wheather
    {
        /// <summary>
        /// Gets or sets UTC foretasted time.
        /// </summary>
        [JsonProperty("dt")]
        public long UtcDateTime { get; set; }

        /// <summary>
        /// Gets or sets temperature in Celsius.
        /// </summary>
        [JsonProperty("main")]
        public TempechureRecord Tempreachure { get; set; }
    }
}
