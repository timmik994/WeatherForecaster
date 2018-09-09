namespace WheatherForecaster.Models.WheatherApiModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Record of the temperature.
    /// </summary>
    public class TempechureRecord
    {
        /// <summary>
        /// Gets or sets temperature.
        /// </summary>
        [JsonProperty("temp")]
        public float Tempeachure { get; set; }
    }
}
