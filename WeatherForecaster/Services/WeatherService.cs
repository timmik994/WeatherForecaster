namespace WheatherForecaster.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WheatherForecaster.Models;

    /// <summary>
    /// Service to work with weather API.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        /// <summary>
        /// Get current whether.
        /// </summary>
        /// <returns>>Weather record with actual values.</returns>
        public async Task<CurrentWeather> GetCurrentWhetherAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(WeatherApiUrlConstants.ActualWeather);
                string wheatherJson = await response.Content.ReadAsStringAsync();
                CurrentWeather wheatherRecord = JsonConvert.DeserializeObject<CurrentWeather>(wheatherJson);
                return wheatherRecord;
            }
        }

        /// <summary>
        /// Gets whether forecast.
        /// </summary>
        /// <returns>Weather record with forecasts.</returns>
        public async Task<WeatherApiListRecord> GetForecastAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(WeatherApiUrlConstants.Forecast);
                string wheatherJson = await response.Content.ReadAsStringAsync();
                WeatherApiListRecord wheatherRecord = JsonConvert.DeserializeObject<WeatherApiListRecord>(wheatherJson);
                return wheatherRecord;
            }
        }
    }
}
