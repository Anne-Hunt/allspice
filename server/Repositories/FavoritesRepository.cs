



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
        string sql = "DELETE * FROM favorites WHERE id = @favoriteId LIMIT 1;";
        _db.Execute(sql);
    }

    internal Favorite CreateFavorite(int recipeId, string userId)
    {
        string accountId = userId;

        string sql = @"
        INSERT INTO favorites(
            recipeId,
            accountId
        )VALUES(
            @RecipeId,
            @AccountId
        );
        
        SELECT 
        favorites.*,
        recipe.*,
        account.*
        WHERE favorites.Id = LAST_INSERT_ID
        JOIN recipes ON recipeId = recipes.Id
        JOIN accounts on accounts.Id = accountId;";

        Favorite favorite = _db.Query(sql, new { recipeId, accountId }).FirstOrDefault();
        return favorite;
    }
}