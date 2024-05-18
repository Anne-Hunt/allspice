namespace allspice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;
    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Favorite> GetFavorites(string userId)
    {
        string sql = "SELECT * FROM favorites WHERE accountId = @userId;";
        List<Favorite> favorites = _db.Query<Favorite>(sql).ToList();
        return favorites;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";
        Favorite favorite = _db.Query<Favorite>(sql).FirstOrDefault();
        return favorite;
    }

    internal void TrashFavorite(int favoriteId)
    {
        string sql = "DELETE FROM favorites WHERE favorites.Id = @favoriteId LIMIT 1;";
        _db.Execute(sql);
    }

    internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO favorites(
            recipeId,
            creatorId
        )VALUES(
            @RecipeId,
            @creatorId
        );
        
        SELECT 
        favorites.*,
        accounts.*,
        recipes.*
        FROM recipes
        JOIN favorites ON recipes.Id = recipeId
        JOIN accounts ON recipes.CreatorId = accounts.Id
        WHERE favorites.Id = LAST_INSERT_ID();";

        FavoriteRecipe favorite = _db.Query<Favorite, Profile, FavoriteRecipe, FavoriteRecipe>(sql, (favorite, profile, recipe) =>
        {
            recipe.FavoriteId = favorite.Id;
            favorite.RecipeId = recipe.Id;
            recipe.Creator = profile;
            return recipe;
        }, favoriteData).FirstOrDefault();
        return favorite;
    }
}