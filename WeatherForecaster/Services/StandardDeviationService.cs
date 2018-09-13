namespace WeatherForecaster.Services
{
    using System.Collections.Generic;
    using WeatherForecaster.Models;

    /// <summary>
    /// Service to get standard deviation for weather forecasts.
    /// </summary>
    public class StandardDeviationService : IStandardDeviationService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private WeatherDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardDeviationService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public StandardDeviationService(WeatherDbContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Gets standard deviations.
        /// </summary>
        /// <returns>Collection with deviations from database.</returns>
        public IEnumerable<StandardDeviation> GetDeviations()
        {
            return this.dbContext.Deviations;
        }
    }
}
