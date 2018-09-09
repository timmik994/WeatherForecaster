using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheatherForecaster.Models;
using WhetherForecaster.Models;

namespace WhetherForecaster.Services
{
    public class StandardDeviationService : IStandartDeviationService
    {
        private WheatherDbContext dbContext;

        private IWeatherService wheatherService;

        public StandardDeviationService(WheatherDbContext context, IWeatherService wheatherService)
        {
            this.dbContext = context;
            this.wheatherService = wheatherService;
        }

        public IEnumerable<StandartDeviation> GetDeviation()
        {
            return dbContext.Deviations;
        }
    }
}
