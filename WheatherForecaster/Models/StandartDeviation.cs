using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WheatherForecaster.Models
{
    /// <summary>
    /// Standard deviation for weather forecast.
    /// </summary>
    public class StandartDeviation
    {
        /// <summary>
        /// Gets or sets id of deviation.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets forecast prediction time.
        /// </summary>
        public int HoursForward { get; set; }

        /// <summary>
        /// Gets or sets deviation.
        /// </summary>
        public float Deviation { get; set; }

        /// <summary>
        /// Gets or sets standard deviation calculation time.
        /// </summary>
        public DateTime CalculationTime { get; set; }
    }
}
