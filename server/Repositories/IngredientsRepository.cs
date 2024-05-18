
namespace allspice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO 
        ingredients(
            quantity,
            name,
            recipeId
        )VALUES(
            @Quantity,
            @Name,
            @RecipeId
        );

        SELECT *
        FROM ingredients
        WHERE ingredients.id = LAST_INSERT_ID()
        ;";
        Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
        return ingredient;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = "SELECT * FROM ingredients WHERE id = @ingredientId;";
        Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetIngredients(int recipeId)
    {
        string sql = @"SELECT * FROM ingredients WHERE recipeId = @recipeId;";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ingredients;
    }

    internal void TrashIngredient(int ingredientId)
    {
        string sql = "DELETE FROM ingredients WHERE ingredients.id = @ingredientId;";
        _db.Execute(sql, new { ingredientId });
    }
}