namespace ServerLibraryProject.Interfaces
{
    using System.Threading.Tasks;
    using ServerLibraryProject.Models;

    public interface IMealRepository
    {
        Task<int> CreateMealAsync(Meal meal, int cookingSkillId, int mealTypeId);

        Task<int> AddMealIngredientAsync(int mealId, int ingredientId, float quantity);

        Task<List<Meal>> GetAllMealsAsync();
    }
}