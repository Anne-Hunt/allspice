
using Microsoft.AspNetCore.Http.HttpResults;

namespace allspice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repository;

    public RecipesService(RecipesRepository repository)
    {
        _repository = repository;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repository.CreateRecipe(recipeData);
        return recipe;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _repository.GetRecipeById(recipeId);
        if (recipe == null)
        {
            throw new Exception("Cannot find that recipe, sorry!");
        }
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _repository.GetRecipes();
        return recipes;
    }

    internal Recipe UpdateRecipe(int recipeId, Recipe recipeData, string userId)
    {
        Recipe recipetoUpdate = GetRecipeById(recipeId);
        if (recipetoUpdate.CreatorId != userId)
        {
            throw new Exception("This recipe isn't yours!");
        }
        recipetoUpdate.Title = recipeData.Title ?? recipetoUpdate.Title;
        recipetoUpdate.Instructions = recipeData.Instructions ?? recipetoUpdate.Instructions;
        recipetoUpdate.Img = recipeData.Img ?? recipetoUpdate.Img;
        recipetoUpdate.Category = recipeData.Category ?? recipetoUpdate.Category;
        Recipe recipe = _repository.UpdateRecipe(recipetoUpdate);
        return recipe;
    }

    internal string TrashRecipe(int recipeId, string userId)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != userId)
        {
            throw new Exception("You cannot delete what is not yours!");
        }
        _repository.TrashRecipe(recipeId);
        return "Recipe removed from database!";
    }

    internal List<RecipeFan> GetRecipesWithFavorites(string userId)
    {
        if (userId == null)
        {
            GetRecipes();
        }
        List<RecipeFan> recipes = _repository.GetRecipesWithFavorites(userId);
        return recipes;
    }

    internal List<Recipe> SearchRecipes(string searchQuery)
    {
        List<Recipe> recipes = _repository.SearchRecipes(searchQuery);
        return recipes;
    }

    internal List<Recipe> GetUserRecipes(string userId)
    {
        List<Recipe> recipes = _repository.GetUserRecipes(userId);
        return recipes;
    }
}