using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhetherForecaster.Models
{
    public class City
    {
        /// <summary>
        /// Gets or sets id of the city in API service.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets city name.
        /// </summary>
        public string Name { get; set; }
    }
}
