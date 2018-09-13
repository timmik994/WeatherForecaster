namespace WeatherForecaster.Services
{
    /// <summary>
    /// Parameters of weather forecast.
    /// </summary>
    public static class ForecastConstants
    {
        /// <summary>
        /// Forecast step size.
        /// </summary>
        public const int ForecastTimeStep = 3;

        /// <summary>
        /// Minimum delta between times when forecast was made and forecasted weather came.
        /// </summary>
        public const int ForecastMinimumTimeDelta = 3;

        /// <summary>
        /// Maximum delta between times when forecast was made and forecasted weather came.
        /// </summary>
        public const int ForecastMaximumTimeDelta = 120;
    }
}
