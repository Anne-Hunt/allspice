

namespace allspice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repository;

    public FavoritesService(FavoritesRepository repository)
    {
        _repository = repository;
    }

    internal List<Favorite> GetFavorites(string userId)
    {
        List<Favorite> favorites = _repository.GetFavorites(userId);
        return favorites;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        Favorite favorite = _repository.GetFavoriteById(favoriteId);
        if (favorite == null)
        {
            throw new Exception("Unable to find!");
        }
        return favorite;
    }

    internal FavoriteRecipe CreateFavorite(Favorite favoriteData)
    {
        FavoriteRecipe favorite = _repository.CreateFavorite(favoriteData);
        return favorite;
    }

    internal string TrashFavorite(int favoriteId, string userId)
    {
        Favorite favorite = GetFavoriteById(favoriteId);
        if (favorite.CreatorId != userId)
        {
            throw new Exception("You cannot delete what you don't have!");
        }
        _repository.TrashFavorite(favoriteId);
        return "Removed favorite!";
    }
}