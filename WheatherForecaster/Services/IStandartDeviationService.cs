namespace WheatherForecaster.Services
{
    using System.Collections.Generic;
    using WheatherForecaster.Models;

    /// <summary>
    /// Service to calculate standard deviation for whether.
    /// </summary>
    public interface IStandartDeviationService
    {
        /// <summary>
        /// Gets standard deviation for whether forecasts.
        /// </summary>
        /// <returns>List of standard deviation.</returns>
        IEnumerable<StandartDeviation> GetDeviation();
    }
}
