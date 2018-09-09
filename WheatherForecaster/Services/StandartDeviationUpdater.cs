
namespace WheatherForecaster.Services
{
    using System;
    using System.Linq;
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using WheatherForecaster.Models;
    using WhetherForecaster.Models;

    /// <summary>
    /// Updater to update standard deviations.
    /// </summary>
    public class StandartDeviationUpdater
    {
        /// <summary>
        /// Default standard deviation value.
        /// </summary>
        private const float DefaultStandartDeviation = 3;

        /// <summary>
        /// Minimum forecast time.
        /// </summary>
        private const int ForecastMinHoursForward = 3;

        /// <summary>
        /// Max forecast time.
        /// </summary>
        private const int ForecastMaxHoursForward = 120;

        /// <summary>
        /// Time between forecasts.
        /// </summary>
        private const int ForecastStep = 3;

        /// <summary>
        /// Interval between updates.
        /// </summary>
        private const int DeviationUpdateInterval = 86400;

        /// <summary>
        /// Context to connect database.
        /// </summary>
        private WheatherDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of <see cref="StandartDeviationUpdater"/> class.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public StandartDeviationUpdater(IServiceProvider serviceProvider)
        {
            IServiceScope scope = serviceProvider.CreateScope();
            this.dbContext = (WheatherDbContext) scope.ServiceProvider.GetService(typeof(WheatherDbContext));
        }

        /// <summary>
        /// Runs deviation update.
        /// </summary>
        public void UpdateDeviation()
        {
            while (true)
            {
                for (int i = StandartDeviationUpdater.ForecastMinHoursForward;
                    i <= StandartDeviationUpdater.ForecastMaxHoursForward;
                    i += StandartDeviationUpdater.ForecastStep)
                {
                    CalculateDeviation(i);
                }

                Thread.Sleep(StandartDeviationUpdater.DeviationUpdateInterval);
            }
        }

        /// <summary>
        /// Calculates standard deviation.
        /// </summary>
        /// <param name="hoursFowward"></param>
        private void CalculateDeviation(int hoursFowward)
        {
            StandartDeviation deviation = 
                dbContext.Deviations.FirstOrDefault(d => d.HoursForward == hoursFowward);
            if (deviation == null)
            {
                deviation = new StandartDeviation()
                {
                    HoursForward = hoursFowward,
                    Deviation = StandartDeviationUpdater.DefaultStandartDeviation
                };
                dbContext.Deviations.Add(deviation);
            }

            IQueryable<WheatherRecord> wheatherRecords =
                dbContext.WhetherRecords.Where(wr => wr.IsFull && wr.ForecastHours == hoursFowward);
            if (wheatherRecords.Count() > 0)
            {
                deviation.Deviation =
                    wheatherRecords.Sum(wr => MathF.Pow(MathF.Abs(wr.ActualTempreche - wr.ForecastTempreche), 2)) /
                    wheatherRecords.Count();
            }

            deviation.CalculationTime = DateTime.Now;
            dbContext.SaveChanges();
        }
    }
}
