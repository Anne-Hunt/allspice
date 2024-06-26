using System.Data.Common;

namespace allspice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repository;
    private readonly RecipesService _recipeService;

    public IngredientsService(IngredientsRepository repository, RecipesService recipeService)
    {
        _repository = repository;
        _recipeService = recipeService;
    }

    internal List<Ingredient> GetIngredients(int recipeId)
    {
        List<Ingredient> ingredients = _repository.GetIngredients(recipeId);
        return ingredients;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _repository.GetIngredientById(ingredientId);
        if (ingredient == null)
        {
            throw new Exception("Unable to find!");
        }
        return ingredient;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
    {
        int RecipeId = ingredientData.RecipeId;
        Recipe recipe = _recipeService.GetRecipeById(RecipeId);
        string creatorId = recipe.CreatorId;
        if (userId != creatorId)
        {
            throw new Exception("You cannot change what you have not made!");
        }
        Ingredient ingredient = _repository.CreateIngredient(ingredientData);
        return ingredient;
    }

    internal string TrashIngredient(int ingredientId, string userId)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        int recipeId = ingredient.RecipeId;
        Recipe recipe = _recipeService.GetRecipeById(recipeId);
        string creatorId = recipe.CreatorId;
        if (userId != creatorId)
        {
            throw new Exception("You cannot delete what you have not made!");
        }
        _repository.TrashIngredient(ingredientId);
        return "Ingredient removed";
    }
}