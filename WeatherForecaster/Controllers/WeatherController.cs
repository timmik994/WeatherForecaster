namespace WeatherForecaster.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WeatherForecaster.Models;
    using WeatherForecaster.Services;

    /// <summary>
    /// Weather controller.
    /// </summary>
    public class WeatherController : Controller
    {
        /// <summary>
        /// Minimum time delta for which we can use standard deviation.
        /// </summary>
        private const int MinimumTimeDeltaWithDeviation = 2;

        /// <summary>
        /// The weather service.
        /// </summary>
        private IWeatherService weatherService;

        /// <summary>
        /// Service to get standard deviation.
        /// </summary>
        private IStandardDeviationService standardDeviationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherController"/> class.
        /// </summary>
        /// <param name="weatherService">Service to connect weather API.</param>
        /// <param name="deviationService">Service to get standard deviation.</param>
        public WeatherController(IWeatherService weatherService, IStandardDeviationService deviationService)
        {
            this.weatherService = weatherService;
            this.standardDeviationService = deviationService;
        }

        /// <summary>
        /// Gets application home page.
        /// </summary>
        /// <returns>The action result.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Gets weather data.
        /// </summary>
        /// <returns>The action result with weather data.</returns>
        public async Task<IActionResult> GetForecastAsync()
        {
            WeatherForecastResponse weatherForecast = await this.weatherService.GetForecastAsync();
            IEnumerable<StandardDeviation> deviations = this.standardDeviationService.GetDeviations();
            IEnumerable<WeatherViewModel> viewModels = weatherForecast.WeatherData.Select(wd =>
            {
                int forecastTime = this.GetForecastMadeForecastCameTimeDelta(wd.DateTime);
                float deviation = 0;
                if (forecastTime >= 2)
                {
                    deviation = deviations.FirstOrDefault(d => d.ForecastMadeForecastCameTimeDelta == forecastTime).Deviation;
                }

                WeatherViewModel viewModel = new WeatherViewModel()
                {
                    ForecastTime = DateTimeHelper.FromUnixUTCTime(wd.DateTime),
                    ActualTemperature = wd.TemperatureRecord.Temperature,
                    MaxTemperature = wd.TemperatureRecord.Temperature + deviation,
                    MinTemperature = wd.TemperatureRecord.Temperature - deviation
                };
                return viewModel;
            });

            return this.Json(viewModels);
        }

        /// <summary>
        /// Gets delta between time when forecast data accepted and forecasted weather is coming.
        /// </summary>
        /// <param name="forecastUnixTime">Forecast time in UNIX format.</param>
        /// <returns>Delta in hours.</returns>
        private int GetForecastMadeForecastCameTimeDelta(long forecastUnixTime)
        {
            DateTime forecastTime = DateTimeHelper.FromUnixUTCTime(forecastUnixTime);
            int delta = DateTimeHelper.GetDeltaInHours(DateTime.Now, forecastTime);
            int timeDeltaInHours = DateTimeHelper.LeadDeltaToValueDivisibleByStep(delta);
            return timeDeltaInHours;
        }
    }
}