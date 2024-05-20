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
        accounts_1.*,
        favorites.*,
        accounts_2.*
        FROM recipes
        JOIN accounts AS accounts_1
        ON accounts_1.Id = recipes.CreatorId
        JOIN favorites
        ON recipes.Id = favorites.RecipeId
        JOIN accounts AS accounts_2
        ON favorites.CreatorId = accounts_2.Id";

        List<RecipeFan> recipes = _db.Query<RecipeFan, Profile, Favorite, Profile, RecipeFan>(sql, (recipeFan, profile, favorite, fan) =>
        {
            recipeFan.Creator = profile;
            recipeFan.Id = favorite.RecipeId;
            recipeFan.Favorite = favorite;
            recipeFan.Fan = profile;
            return recipeFan;
        }, new { accountId }).ToList();
        return recipes;
    }
}