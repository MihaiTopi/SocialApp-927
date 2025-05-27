namespace ServerLibraryProject.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ServerLibraryProject.Models;

    public interface IMealService
    {
        Task<bool> CreateMealWithCookingLevelAsync(Meal mealToCreate, string cookingLevelDescription);

        Task<List<Meal>> RetrieveAllMealsAsync();

        Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName);

        Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity);
    }
}