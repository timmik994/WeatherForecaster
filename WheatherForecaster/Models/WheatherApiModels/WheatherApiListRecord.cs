namespace WheatherForecaster.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Whether response from API.
    /// </summary>
    public class WheatherApiListRecord
    {
        /// <summary>
        /// Gets or sets city where we get forecast.
        /// </summary>
        [JsonProperty("city")]
        public City City { get; set; }

        /// <summary>
        /// Gets or sets list of whether records.
        /// </summary>
        [JsonProperty("list")]
        public IEnumerable<Wheather> WhetherData { get; set; }
    }
}
