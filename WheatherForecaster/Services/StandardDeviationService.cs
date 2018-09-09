namespace WheatherForecaster.Services
{
    using System.Collections.Generic;
    using WheatherForecaster.Models;

    /// <summary>
    /// Service to get standard deviation data.
    /// </summary>
    public class StandardDeviationService : IStandartDeviationService
    {
        /// <summary>
        /// Context to connect database.
        /// </summary>
        private WeatherDbContext dbContext;

        /// <summary>
        /// Service to get weather data.
        /// </summary>
        private IWeatherService wheatherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardDeviationService"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="wheatherService">Service to work with weather API.</param>
        public StandardDeviationService(WeatherDbContext context, IWeatherService wheatherService)
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
            return this.dbContext.Deviations;
        }
    }
}
