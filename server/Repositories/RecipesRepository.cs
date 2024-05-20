namespace allspice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO
        recipes(
            title,
            instructions,
            img,
            category,
            creatorId
        )VALUES(
            @Title,
            @Instructions,
            @Img,
            @Category,
            @CreatorId
        );
        
        SELECT 
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON accounts.id = recipes.creatorId
        WHERE recipes.id = LAST_INSERT_ID();";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, recipeData).FirstOrDefault();
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        string sql = @"SELECT 
        recipes.*,
        accounts.* 
        FROM recipes 
        JOIN accounts on accounts.id = recipes.creatorId;";

        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }).ToList();
        return recipes;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"SELECT
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts on accounts.id = recipes.creatorId
        WHERE recipes.id = @recipeId;";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, new { recipeId }).FirstOrDefault();
        return recipe;
    }

    internal Recipe UpdateRecipe(Recipe recipetoUpdate)
    {
        string sql = @"
        UPDATE recipes
        SET
        instructions = @Instructions,
        title = @Title,
        img = @Img,
        category = @Category
        WHERE id = @Id;


        SELECT
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON accounts.id = recipes.creatorId
        WHERE recipes.id = @Id;";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;
            return recipe;
        }, recipetoUpdate).FirstOrDefault();
        return recipe;
    }

    internal void TrashRecipe(int recipeId)
    {
        string sql = "DELETE FROM recipes WHERE recipes.id = @recipeId;";
        _db.Execute(sql, new { recipeId });
    }

    internal List<RecipeFan> GetRecipesWithFavorites(string accountId)
    {
        string sql = @"
        SELECT
        recipes.*,
        favorites.*,
        accounts.*
        FROM 
        recipes
        JOIN favorites
        ON favorites.RecipeId = recipes.Id
        JOIN accounts 
        ON accounts.Id = recipes.CreatorId
        JOIN accounts
        ON favorites.CreatorId = @accountId;";

        List<RecipeFan> recipes = _db.Query<RecipeFan, Favorite, Profile, Profile, RecipeFan>(sql, (recipeFan, favorite, profile, fan) =>
        {
            recipeFan.Id = favorite.RecipeId;
            recipeFan.Creator = profile;
            recipeFan.Fan = profile;
            return recipeFan;
        }, new { accountId }).ToList();
        return recipes;
    }
}