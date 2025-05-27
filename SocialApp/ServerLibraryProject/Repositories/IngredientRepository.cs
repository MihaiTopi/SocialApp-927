namespace ServerLibraryProject.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using ServerLibraryProject.Data;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;

    public class IngredientRepository : IIngredientRepository
    {
        private readonly SocialAppDbContext dbContext;

        public IngredientRepository(SocialAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Ingredient GetIngredientByName(string name)
        {
            var ingredient = this.dbContext.Ingredients
                .AsNoTracking()
                .FirstOrDefault(i => i.Name == name);

            // Optional fallback if not found
            return ingredient ?? Ingredient.NoIngredient;
        }
    }
}