
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
}