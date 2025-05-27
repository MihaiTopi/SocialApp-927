namespace ServerLibraryProject.Interfaces
{
    using ServerLibraryProject.Models;

    public interface ICalorieRepository
    {
        Calorie? GetCaloriesByUserId(long userId);
    }
}