namespace WheatherForecaster.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using WheatherForecaster.Models;

    /// <summary>
    /// Service to work with weather API.
    /// </summary>
    public class WheatherService : IWeatherService
    {
        /// <summary>
        /// Get current whether.
        /// </summary>
        /// <returns>>Weather record with actual values.</returns>
        public async Task<CurrentWheather> GetCurrentWhetherAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(WeatherApiUrlConstants.ActualWeather);
                string wheatherJson = await response.Content.ReadAsStringAsync();
                CurrentWheather wheatherRecord = JsonConvert.DeserializeObject<CurrentWheather>(wheatherJson);
                return wheatherRecord;
            }
        }

        /// <summary>
        /// Gets whether forecast.
        /// </summary>
        /// <returns>Weather record with forecasts.</returns>
        public async Task<WheatherApiListRecord> GetForecastAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(WeatherApiUrlConstants.Forecast);
                string wheatherJson = await response.Content.ReadAsStringAsync();
                WheatherApiListRecord wheatherRecord = JsonConvert.DeserializeObject<WheatherApiListRecord>(wheatherJson);
                return wheatherRecord;
            }
        }
    }
}
