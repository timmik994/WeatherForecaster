namespace WeatherForecaster.Services
{
    using System.Collections.Generic;
    using WeatherForecaster.Models;

    /// <summary>
    /// Service to get standard deviation for weather forecasts.
    /// </summary>
    public interface IStandardDeviationService
    {
        /// <summary>
        /// Gets standard deviations for weather forecasts.
        /// </summary>
        /// <returns>List of standard deviation.</returns>
        IEnumerable<StandardDeviation> GetDeviations();
    }
}
