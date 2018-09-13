namespace WeatherForecaster.Services
{
    using System.Threading.Tasks;
    using WeatherForecaster.Models;

    /// <summary>
    /// Client to weather API.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Gets weather forecast.
        /// </summary>
        /// <returns>Weather forecast for 5 days.</returns>
        Task<WeatherForecastResponse> GetForecastAsync();

        /// <summary>
        /// Get current weather.
        /// </summary>
        /// <returns>>Current weather data.</returns>
        Task<WeatherResponse> GetCurrentWeatherAsync();
    }
}
