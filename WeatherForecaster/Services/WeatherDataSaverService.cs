namespace WeatherForecaster.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using WeatherForecaster.Models;

    /// <summary>
    /// Service that collects data about weather forecasts and actual weather from API.
    /// </summary>
    public class WeatherDataSaverService
    {
        /// <summary>
        /// Interval between data requests (in milliseconds).
        /// </summary>
        private const int UpdateIntervalInMilliseconds = 10800000;

        /// <summary>
        /// The database context.
        /// </summary>
        private WeatherDbContext dbContext;

        /// <summary>
        /// Service to get weather data.
        /// </summary>
        private IWeatherService weatherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherDataSaverService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="weatherService">Weather API client.</param>
        public WeatherDataSaverService(IServiceProvider serviceProvider, IWeatherService weatherService)
        {
            IServiceScope scope = serviceProvider.CreateScope();
            this.dbContext = 
                (WeatherDbContext)scope.ServiceProvider.GetService(typeof(WeatherDbContext));
            this.weatherService = weatherService;
        }

        /// <summary>
        /// Saves weather data to database.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task SaveWeatherData()
        {
            while (true)
            {
                await this.SaveNewForecasts();
                await this.UpdateActualWeatherData();
                Thread.Sleep(WeatherDataSaverService.UpdateIntervalInMilliseconds);
            }
        }

        /// <summary>
        /// Gets and saves forecast data in database.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task SaveNewForecasts()
        {
            WeatherForecastResponse weatherForecast = await this.weatherService.GetForecastAsync();
            IEnumerable<WeatherRecord> weatherRecords = weatherForecast.WeatherData.Select(wd =>
            {
                DateTime forecastTime = DateTimeHelper.FromUnixUTCTime(wd.DateTime);
                int forecastNowTimeDelta =
                    DateTimeHelper.GetDeltaInHours(DateTime.Now, forecastTime);
                var record = new WeatherRecord()
                {
                    ForecastTime = forecastTime,
                    IsFull = false,
                    ForecastTemperature = wd.TemperatureRecord.Temperature,
                    ForecastMadeForecastCameTimeDelta = 
                        DateTimeHelper.LeadDeltaToValueDivisibleByStep(forecastNowTimeDelta)
                };
                return record;
            });
            this.dbContext.AddRange(weatherRecords);
            await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Gets and saves actual weather data to database.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task UpdateActualWeatherData()
        {
            WeatherResponse weather = await this.weatherService.GetCurrentWeatherAsync();
            DateTime weatherTime = DateTimeHelper.FromUnixUTCTime(weather.DateTime);
            IQueryable<WeatherRecord> recordsToUpdate = this.dbContext.WeatherRecords.Where(wr =>
                DateTimeHelper.GetDeltaInHours(wr.ForecastTime, weatherTime) == 1);
            foreach (var record in recordsToUpdate)
            {
                record.ActualTemperature = weather.TemperatureRecord.Temperature;
                record.IsFull = true;
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
