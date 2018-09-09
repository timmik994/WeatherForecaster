namespace WheatherForecaster.Services
{
    using System.Threading.Tasks;
    using WheatherForecaster.Models;

    /// <summary>
    /// Client to whether API.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Gets whether forecast.
        /// </summary>
        /// <returns>Weather record with forecasts.</returns>
        Task<WheatherApiListRecord> GetForecastAsync();

        /// <summary>
        /// Get current whether.
        /// </summary>
        /// <returns>>Weather record with actual values.</returns>
        Task<CurrentWheather> GetCurrentWhetherAsync();
    }
}
