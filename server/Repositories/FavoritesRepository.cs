


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
}