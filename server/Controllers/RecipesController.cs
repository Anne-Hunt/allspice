using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0Provider;
    private readonly IngredientsService _ingredientsService;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService, FavoritesService favoriteService)
    {
        _recipesService = recipesService;
        _auth0Provider = auth0Provider;
        _ingredientsService = ingredientsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.CreateRecipe(recipeData);
            return Ok(recipe);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetRecipes()
    {
        try
        {
            List<Recipe> recipes = _recipesService.GetRecipes();
            return Ok(recipes);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{recipeId}")]
    public ActionResult<Recipe> GetRecipeById(int recipeId)
    {
        try
        {
            Recipe recipe = _recipesService.GetRecipeById(recipeId);
            return recipe;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpGet("{accountId}/favorites")]
    public async Task<ActionResult<List<RecipeFan>>> GetRecipesWithFavorites(string accountId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<RecipeFan> recipes = _recipesService.GetRecipesWithFavorites(user.Id);
            return recipes;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{recipeId}")]
    public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipeData, int recipeId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Recipe updatedRecipe = _recipesService.UpdateRecipe(recipeId, recipeData, user.Id);
            return updatedRecipe;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{recipeId}")]

    public async Task<ActionResult<string>> TrashRecipe(int recipeId)
    {
        try
        {
            Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string memo = _recipesService.TrashRecipe(recipeId, user.Id);
            return Ok(memo);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{recipeId}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredients(int recipeId)
    {
        try
        {
            List<Ingredient> ingredients = _ingredientsService.GetIngredients(recipeId);
            return Ok(ingredients);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}