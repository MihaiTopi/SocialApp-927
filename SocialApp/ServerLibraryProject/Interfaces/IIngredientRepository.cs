namespace ServerLibraryProject.Interfaces
{
    using ServerLibraryProject.Models;

    public interface IIngredientRepository
    {
        // Async method to fetch ingredient details by name
        Ingredient GetIngredientByName(string name);
    }
}