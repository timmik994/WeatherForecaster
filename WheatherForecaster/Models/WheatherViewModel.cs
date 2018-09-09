﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WheatherForecaster.Models
{
    /// <summary>
    /// Data model of weather forecast to return from service.
    /// </summary>
    public class WheatherViewModel
    {
        /// <summary>
        /// Gets or sets max temperature using standard deviation.
        /// </summary>
        public float MaxTemperature { get; set; }

        /// <summary>
        /// Gets or sets actual temperature in forecast.
        /// </summary>
        public float ActualTemperature { get; set; }

        /// <summary>
        /// Gets or sets min temperature using standard deviation.
        /// </summary>
        public float MinTemperature { get; set; }

        /// <summary>
        /// Gets or sets time of forecast.
        /// </summary>
        public DateTime ForecastTime { get; set; }
    }
}
