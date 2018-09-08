using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhetherForecaster.Models;

namespace WhetherForecaster.Services
{
    public class StandardDeviationService : IStandartDeviationService
    {
        private WheatherDbContext dbContext;

        private IWeatherService wheatherService;

        public StandardDeviationService(WheatherDbContext context, IWeatherService wheatherService)
        {

        }

        public float GetDeviation(int hours)
        {
            throw new NotImplementedException();
        }
    }
}
