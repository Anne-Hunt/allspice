namespace allspice.Models;

public class FavoriteRecipe : Recipe
{
    public int FavoriteId { get; set; }
    public int RecipeId { get; set; }
}