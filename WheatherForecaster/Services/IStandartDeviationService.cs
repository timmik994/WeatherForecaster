using System.Collections.Generic;
using WheatherForecaster.Models;

namespace WhetherForecaster.Services
{
    /// <summary>
    /// Service to calculate standard deviation for whether.
    /// </summary>
    public interface IStandartDeviationService
    {
        /// <summary>
        /// Gets standard deviation for whether forecasts.
        /// </summary>
        /// <param name="hours">Hours forecast forward/param>
        /// <returns>List of standard deviation.</returns>
        IEnumerable<StandartDeviation> GetDeviation();
    }
}
