
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
        INSERT INTO ingredients(
            recipeId,
            quantity,
            name
        )VALUES(
            @RecipeId,
            @Quantity,
            @Name
        );

        SELECT *
        FROM ingredients
        WHERE id = LAST_INSERT_ID
        ;";
        Ingredient ingredient = _db.Query(sql).FirstOrDefault();
        return ingredient;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = "SELECT * FROM ingredients WHERE id = @ingredientId;";
        Ingredient ingredient = _db.Query<Ingredient>(sql).FirstOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetIngredients(int recipeId)
    {
        string sql = @"SELECT * FROM ingredients WHERE recipeId = @recipeId;";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql).ToList();
        return ingredients;
    }

    internal void TrashIngredient(int ingredientId)
    {
        string sql = "DELETE * FROM ingredients WHERE id = @ingredientId LIMIT 1;";
        _db.Execute(sql, new { ingredientId });
    }
}