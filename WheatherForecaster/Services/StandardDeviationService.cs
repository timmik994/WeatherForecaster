

namespace WhetherForecaster.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WheatherForecaster.Models;
    using WhetherForecaster.Models;

    /// <summary>
    /// Service to get standard deviation data.
    /// </summary>
    public class StandardDeviationService : IStandartDeviationService
    {
        /// <summary>
        /// Context to connect database.
        /// </summary>
        private WheatherDbContext dbContext;

        /// <summary>
        /// Service to get weather data.
        /// </summary>
        private IWeatherService wheatherService;

        /// <summary>
        /// Initializes a new instance of <see cref="StandardDeviationService"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="wheatherService"></param>
        public StandardDeviationService(WheatherDbContext context, IWeatherService wheatherService)
        {
            this.dbContext = context;
            this.wheatherService = wheatherService;
        }

        /// <summary>
        /// Gets deviations.
        /// </summary>
        /// <returns>Collection with deviations from DB.</returns>
        public IEnumerable<StandartDeviation> GetDeviation()
        {
            return dbContext.Deviations;
        }
    }
}
