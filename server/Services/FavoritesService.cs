
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
}