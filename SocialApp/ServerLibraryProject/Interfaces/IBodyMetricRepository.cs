namespace ServerLibraryProject.Interfaces
{
    /// <summary>
    /// Repository for managing body metrics.
    /// </summary>
    public interface IBodyMetricRepository
    {
        /// <summary>
        /// Updates the body metrics for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="weight">The user's weight.</param>
        /// <param name="height">The user's height.</param>
        /// <param name="targetWeight">The user's target weight (optional)</param>
        void UpdateUserBodyMetrics(long userId, float weight, float height, float? targetWeight);
    }
}