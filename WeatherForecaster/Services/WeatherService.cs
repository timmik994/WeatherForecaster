namespace WeatherForecaster.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WeatherForecaster.Models;

    /// <summary>
    /// Service to work with weather API.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        /// <summary>
        /// Gets current weather.
        /// </summary>
        /// <returns>>Weather record with actual values.</returns>
        public async Task<WeatherResponse> GetCurrentWeatherAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(WeatherApiUrlConstants.ActualWeather);
                string weatherJson = await response.Content.ReadAsStringAsync();
                WeatherResponse weatherRecord = JsonConvert.DeserializeObject<WeatherResponse>(weatherJson);
                return weatherRecord;
            }
        }

        /// <summary>
        /// Gets weather forecast.
        /// </summary>
        /// <returns>Weather record with forecast.</returns>
        public async Task<WeatherForecastResponse> GetForecastAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(WeatherApiUrlConstants.Forecast);
                string weatherJson = await response.Content.ReadAsStringAsync();
                WeatherForecastResponse weatherApiListRecord = JsonConvert.DeserializeObject<WeatherForecastResponse>(weatherJson);
                return weatherApiListRecord;
            }
        }
    }
}
