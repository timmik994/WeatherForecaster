using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WhetherForecaster.Models
{
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
