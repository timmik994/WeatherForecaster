using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WheatherForecaster.Models;
using WhetherForecaster.Models;

namespace WheatherForecaster.Services
{
    public class StandartDeviationUpdater
    {
        private const float DefaultStandartDeviation = 0;

        private const int ForecastMinHoursForward = 3;

        private const int ForecastMaxHoursForward = 120;

        private const int ForecastStep = 3;

        private const int DeviationUpdateInterval = 86400;

        private WheatherDbContext dbContext;

        public StandartDeviationUpdater(WheatherDbContext context)
        {
            this.dbContext = context;
        }

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
