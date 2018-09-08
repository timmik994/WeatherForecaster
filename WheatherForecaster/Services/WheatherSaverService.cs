using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WheatherForecaster.Models;
using WhetherForecaster.Models;
using WhetherForecaster.Services;

namespace WheatherForecaster.Services
{
    public class WheatherSaverService
    {
        private const int UpdateInterval = 10800;

        /// <summary>
        /// Context to connect database.
        /// </summary>
        private WheatherDbContext dbContext;

        /// <summary>
        /// Service to get weather data.
        /// </summary>
        private IWeatherService wheatherService;


        public WheatherSaverService(WheatherDbContext context, IWeatherService wheatherService)
        {
            this.dbContext = context;
            this.wheatherService = wheatherService;
        }

        public async Task SaveWheatherData()
        {
            while (true)
            {
                await this.SaveNewForecasts();
                await this.UpdateActualWheatherData();
                Thread.Sleep(WheatherSaverService.UpdateInterval);
            }
        }

        private async Task SaveNewForecasts()
        {
            WheatherApiListRecord wheather = await wheatherService.GetForecastAsync();
            IEnumerable<WheatherRecord> wheatherRecords = wheather.WhetherData.Select(wd =>
            {
                DateTime forecastTime = DateTimeHelper.FromUnixUTCTime(wd.UtcDateTime);
                int forecastNowTimeDelta =
                    DateTimeHelper.GetDateDeltaInHours(DateTime.Now, forecastTime);
                var record = new WheatherRecord()
                {
                    ForecastTime = forecastTime,
                    IsFull = false,
                    ForecastTempreche = wd.Tempreachure,
                    ForecastHours = DateTimeHelper.GetForecastHours(forecastNowTimeDelta)
                };
                return record;
            });
            dbContext.AddRange(wheatherRecords);
            await dbContext.SaveChangesAsync();
        }

        private async Task UpdateActualWheatherData()
        {
            CurrentWheather wheather = await wheatherService.GetCurrentWhetherAsync();
            DateTime wheatherTime = DateTimeHelper.FromUnixUTCTime(wheather.DateTime);
            IQueryable<WheatherRecord> recordsToUpdate = dbContext.WhetherRecords.Where(wr =>
                DateTimeHelper.GetDateDeltaInHours(wr.ForecastTime, wheatherTime) == 1);
            foreach (var record in recordsToUpdate)
            {
                record.ActualTempreche = wheather.Tempreachure;
                record.IsFull = true;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
