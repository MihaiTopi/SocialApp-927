namespace ServerLibraryProject.Interfaces
{
    public interface IBodyMetricService
    {
        void UpdateUserBodyMetrics(string username, float weight, float height, float? targetWeight);
    }
}