using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WheatherForecaster.Models.WheatherApiModels;

namespace WheatherForecaster.Models
{
    /// <summary>
    /// Current whether API response.
    /// </summary>
    public class CurrentWheather
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
