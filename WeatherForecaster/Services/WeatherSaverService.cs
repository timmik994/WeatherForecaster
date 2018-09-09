namespace WheatherForecaster.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using WheatherForecaster.Models;


    /// <summary>
    /// Service saves weather data from API.
    /// </summary>
    public class WheatherSaverService
    {
        /// <summary>
        /// Interval between data requests.
        /// </summary>
        private const int UpdateInterval = 10800;

        /// <summary>
        /// Context to connect database.
        /// </summary>
        private WeatherDbContext dbContext;

        /// <summary>
        /// Service to get weather data.
        /// </summary>
        private IWeatherService wheatherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WheatherSaverService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="wheatherService">Weather API client.</param>
        public WheatherSaverService(IServiceProvider serviceProvider, IWeatherService wheatherService)
        {
            IServiceScope scope = serviceProvider.CreateScope();
            this.dbContext = 
                (WeatherDbContext)scope.ServiceProvider.GetService(typeof(WeatherDbContext));
            this.wheatherService = wheatherService;
        }

        /// <summary>
        /// Saves weather data to database.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SaveWheatherData()
        {
            while (true)
            {
                await this.SaveNewForecasts();
                await this.UpdateActualWheatherData();
                Thread.Sleep(WheatherSaverService.UpdateInterval);
            }
        }

        /// <summary>
        /// Gets and saves forecast data in database.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task SaveNewForecasts()
        {
            WeatherApiListRecord wheather = await this.wheatherService.GetForecastAsync();
            IEnumerable<WeatherRecord> wheatherRecords = wheather.WhetherData.Select(wd =>
            {
                DateTime forecastTime = DateTimeHelper.FromUnixUTCTime(wd.UtcDateTime);
                int forecastNowTimeDelta =
                    DateTimeHelper.GetDateDeltaInHours(DateTime.Now, forecastTime);
                var record = new WeatherRecord()
                {
                    ForecastTime = forecastTime,
                    IsFull = false,
                    ForecastTempreche = wd.Tempreachure.Tempeachure,
                    ForecastHours = DateTimeHelper.GetForecastHours(forecastNowTimeDelta)
                };
                return record;
            });
            this.dbContext.AddRange(wheatherRecords);
            await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Sets actual whether data in weather records in database.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task UpdateActualWheatherData()
        {
            CurrentWeather wheather = await this.wheatherService.GetCurrentWhetherAsync();
            DateTime wheatherTime = DateTimeHelper.FromUnixUTCTime(wheather.DateTime);
            IQueryable<WeatherRecord> recordsToUpdate = this.dbContext.WhetherRecords.Where(wr =>
                DateTimeHelper.GetDateDeltaInHours(wr.ForecastTime, wheatherTime) == 1);
            foreach (var record in recordsToUpdate)
            {
                record.ActualTempreche = wheather.Tempreachure.Tempeachure;
                record.IsFull = true;
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
