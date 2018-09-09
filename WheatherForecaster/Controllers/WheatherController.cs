namespace WheatherForecaster.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WheatherForecaster.Models;
    using WheatherForecaster.Services;
    using WhetherForecaster.Models;
    using WhetherForecaster.Services;

    /// <summary>
    /// Controller to work with weatherData.
    /// </summary>
    public class WheatherController : Controller
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
        /// Initializes a new instance of <see cref="WheatherController"/> class.
        /// </summary>
        /// <param name="wheatherService">Service to connect weather API.</param>
        /// <param name="deviationService">Service to get standard deviation.</param>
        public WheatherController(IWeatherService wheatherService, IStandartDeviationService deviationService)
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
            return View();
        }

        /// <summary>
        /// Processes request and returns weather data.
        /// </summary>
        /// <returns>Action result.</returns>
        public async Task<IActionResult> GetForecastAsync()
        {
            WheatherApiListRecord wheatherForecast =await wheatherService.GetForecastAsync();
            IEnumerable<StandartDeviation> deviations = standartDeviationService.GetDeviation();
            IEnumerable<WheatherViewModel> viewModels = wheatherForecast.WhetherData.Select(wd =>
            {
                int forwardTime = this.GetForecastForwardHours(wd.UtcDateTime);
                float deviation = 0;
                if (forwardTime >= 2)
                {
                    deviation = deviations.FirstOrDefault(d => d.HoursForward == forwardTime).Deviation;
                }

                WheatherViewModel viewModel = new WheatherViewModel()
                {
                    ForecastTime = DateTimeHelper.FromUnixUTCTime(wd.UtcDateTime),
                    ActualTemperature = wd.Tempreachure.Tempeachure,
                    MaxTemperature = wd.Tempreachure.Tempeachure + deviation,
                    MinTemperature = wd.Tempreachure.Tempeachure - deviation
                };
                return viewModel;
            });

            return Json(viewModels);
        }

        /// <summary>
        /// Get delta between forecast data accepted and forecast weather time.
        /// </summary>
        /// <param name="forecastUnixTime">UNIX time stamp forward of forecast.</param>
        /// <returns>Delta in hours.</returns>
        private int GetForecastForwardHours(long forecastUnixTime)
        {
            DateTime forecastForwardTime= DateTimeHelper.FromUnixUTCTime(forecastUnixTime);
            int delta = DateTimeHelper.GetDateDeltaInHours(DateTime.Now.ToUniversalTime(), forecastForwardTime);
            int forecastForwardHours = DateTimeHelper.GetForecastHours(delta);
            return forecastForwardHours;
        }
    }
}