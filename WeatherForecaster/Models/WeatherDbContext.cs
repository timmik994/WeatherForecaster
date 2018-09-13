namespace WeatherForecaster.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Database context with weather data.
    /// </summary>
    public class WeatherDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherDbContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets table with weather records.
        /// </summary>
        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        /// <summary>
        /// Gets or sets table with standard deviations.
        /// </summary>
        public DbSet<StandardDeviation> Deviations { get; set; }
    }
}
