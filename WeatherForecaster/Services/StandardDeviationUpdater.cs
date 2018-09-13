namespace WeatherForecaster.Services
{
    using System;
    using System.Linq;
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using WeatherForecaster.Models;

    /// <summary>
    /// Updater to calculate standard deviations using new captured weather data.
    /// </summary>
    public class StandardDeviationUpdater
    {
        /// <summary>
        /// Default standard deviation value.
        /// </summary>
        private const float DefaultStandardDeviation = 3;

        /// <summary>
        /// Interval between updates (in milliseconds).
        /// </summary>
        private const int DeviationUpdateIntervalInMilliseconds = 86400000;

        /// <summary>
        /// Build all the elements in this degree to calculate standard deviation.
        /// </summary>
        private const int StandardDeviationDegree = 2;

        /// <summary>
        /// The database context.
        /// </summary>
        private WeatherDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the<see cref="StandardDeviationUpdater"/> class.
        /// </summary>
        /// <param name="serviceProvider">Provider of services.</param>
        public StandardDeviationUpdater(IServiceProvider serviceProvider)
        {
            IServiceScope scope = serviceProvider.CreateScope();
            this.dbContext = (WeatherDbContext)scope.ServiceProvider.GetService(typeof(WeatherDbContext));
        }

        /// <summary>
        /// Runs standard deviation update.
        /// </summary>
        public void UpdateDeviation()
        {
            while (true)
            {
                for (int i = ForecastConstants.ForecastMinimumTimeDelta;
                    i <= ForecastConstants.ForecastMaximumTimeDelta;
                    i += ForecastConstants.ForecastTimeStep)
                {
                    this.CalculateDeviation(i);
                }

                Thread.Sleep(StandardDeviationUpdater.DeviationUpdateIntervalInMilliseconds);
            }
        }

        /// <summary>
        /// Calculates standard deviation.
        /// </summary>
        /// <param name="forecastMadeForecastCameTimeDelta">Delta between time when forecast data accepted and forecasted weather comes.</param>
        private void CalculateDeviation(int forecastMadeForecastCameTimeDelta)
        {
            StandardDeviation deviation = 
                this.dbContext.Deviations.FirstOrDefault(d => d.ForecastMadeForecastCameTimeDelta == forecastMadeForecastCameTimeDelta);
            if (deviation == null)
            {
                deviation = new StandardDeviation()
                {
                    ForecastMadeForecastCameTimeDelta = forecastMadeForecastCameTimeDelta,
                    Deviation = StandardDeviationUpdater.DefaultStandardDeviation
                };
                this.dbContext.Deviations.Add(deviation);
            }

            IQueryable<WeatherRecord> weatherRecords =
                this.dbContext.WeatherRecords.Where(wr => 
                    wr.IsFull && wr.ForecastMadeForecastCameTimeDelta == forecastMadeForecastCameTimeDelta);
            if (weatherRecords.Count() > 0)
            {
                deviation.Deviation =
                    weatherRecords.Sum(wr => MathF.Pow(
                        MathF.Abs(wr.ActualTemperature - wr.ForecastTemperature),
                        StandardDeviationUpdater.StandardDeviationDegree)) /
                    weatherRecords.Count();
            }

            deviation.CalculationTime = DateTime.Now;
            this.dbContext.SaveChanges();
        }
    }
}
