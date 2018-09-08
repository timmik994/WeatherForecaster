using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WheatherForecaster.Models
{
    public class CurrentWheather
    {
        [JsonProperty("dt")]
        public long DateTime { get; set; }

        [JsonProperty("main.temp")]
        public float Tempreachure { get; set; }
    }
}
