using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WheatherForecaster.Models.WheatherApiModels
{
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
