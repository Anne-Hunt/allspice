namespace allspice.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0Provider;

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
    {
        _favoritesService = favoritesService;
        _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public async Task<ActionResult<List<Favorite>>> GetFavorites()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            List<Favorite> favorites = _favoritesService.GetFavorites(userId);
            return favorites;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPost]

    public async Task<ActionResult<FavoriteRecipe>> CreateFavorite([FromBody] Favorite favoriteData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            favoriteData.CreatorId = userInfo.Id;
            FavoriteRecipe favorite = _favoritesService.CreateFavorite(favoriteData);
            return favorite;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}