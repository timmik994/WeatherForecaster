using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WheatherForecaster.Models;
using WheatherForecaster.Services;
using WhetherForecaster.Models;
using WhetherForecaster.Services;

namespace WheatherForecaster.Controllers
{
    public class WheatherController : Controller
    {
        private IWeatherService wheatherService;

        private IStandartDeviationService standartDeviationService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetForecastAsync()
        {
            WheatherApiListRecord wheatherForecast =await wheatherService.GetForecastAsync();
            IEnumerable<StandartDeviation> deviations = standartDeviationService.GetDeviation();
            IEnumerable<WheatherViewModel> viewModels = wheatherForecast.WhetherData.Select(wd =>
            {
                int forwardTime = this.GetForecastForwardHours(wd.UtcDateTime);
                float deviation = deviations.FirstOrDefault(d => d.HoursForward == forwardTime).Deviation;
                WheatherViewModel viewModel = new WheatherViewModel()
                {
                    ForecastTime = DateTimeHelper.FromUnixUTCTime(wd.UtcDateTime),
                    ActualTemperature = wd.Tempreachure,
                    MaxTemperature = wd.Tempreachure + deviation,
                    MinTemperature = wd.Tempreachure - deviation
                };
                return viewModel;
            });

            return Json(viewModels);
        }

        private int GetForecastForwardHours(long forecastUnixTime)
        {
            DateTime forecastForwardTime= DateTimeHelper.FromUnixUTCTime(forecastUnixTime);
            int delta = DateTimeHelper.GetDateDeltaInHours(DateTime.Now.ToUniversalTime(), forecastForwardTime);
            int forecastForwardHours = DateTimeHelper.GetForecastHours(delta);
            return forecastForwardHours;
        }
    }
}