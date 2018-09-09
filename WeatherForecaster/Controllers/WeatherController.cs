namespace WheatherForecaster.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WheatherForecaster.Models;
    using WheatherForecaster.Services;


    /// <summary>
    /// Controller to work with weatherData.
    /// </summary>
    public class WeatherController : Controller
    {
        /// <summary>
        /// Service to get weather data.
        /// </summary>
        private IWeatherService wheatherService;

        /// <summary>
        /// Service to get standard deviation.
        /// </summary>
        private IStandartDeviationService standartDeviationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherController"/> class.
        /// </summary>
        /// <param name="wheatherService">Service to connect weather API.</param>
        /// <param name="deviationService">Service to get standard deviation.</param>
        public WeatherController(IWeatherService wheatherService, IStandartDeviationService deviationService)
        {
            this.wheatherService = wheatherService;
            this.standartDeviationService = deviationService;
        }

        /// <summary>
        /// Processes request to Index URL.
        /// </summary>
        /// <returns>Action result.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Processes request and returns weather data.
        /// </summary>
        /// <returns>Action result.</returns>
        public async Task<IActionResult> GetForecastAsync()
        {
            WeatherApiListRecord wheatherForecast = await this.wheatherService.GetForecastAsync();
            IEnumerable<StandartDeviation> deviations = this.standartDeviationService.GetDeviation();
            IEnumerable<WeatherViewModel> viewModels = wheatherForecast.WhetherData.Select(wd =>
            {
                int forwardTime = this.GetForecastForwardHours(wd.UtcDateTime);
                float deviation = 0;
                if (forwardTime >= 2)
                {
                    deviation = deviations.FirstOrDefault(d => d.HoursForward == forwardTime).Deviation;
                }

                WeatherViewModel viewModel = new WeatherViewModel()
                {
                    ForecastTime = DateTimeHelper.FromUnixUTCTime(wd.UtcDateTime),
                    ActualTemperature = wd.Tempreachure.Tempeachure,
                    MaxTemperature = wd.Tempreachure.Tempeachure + deviation,
                    MinTemperature = wd.Tempreachure.Tempeachure - deviation
                };
                return viewModel;
            });

            return this.Json(viewModels);
        }

        /// <summary>
        /// Get delta between forecast data accepted and forecast weather time.
        /// </summary>
        /// <param name="forecastUnixTime">UNIX time stamp forward of forecast.</param>
        /// <returns>Delta in hours.</returns>
        private int GetForecastForwardHours(long forecastUnixTime)
        {
            DateTime forecastForwardTime = DateTimeHelper.FromUnixUTCTime(forecastUnixTime);
            int delta = DateTimeHelper.GetDateDeltaInHours(DateTime.Now.ToUniversalTime(), forecastForwardTime);
            int forecastForwardHours = DateTimeHelper.GetForecastHours(delta);
            return forecastForwardHours;
        }
    }
}