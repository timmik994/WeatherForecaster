namespace WhetherForecaster.Services
{
    /// <summary>
    /// Service to calculate standard deviation for whether.
    /// </summary>
    public interface IStandartDeviationService
    {
        /// <summary>
        /// Gets standard deviation for whether forecasts.
        /// </summary>
        /// <param name="hours">Hours forecast forward/param>
        /// <returns></returns>
        float GetDeviation(int hours);
    }
}
