namespace allspice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoritesService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoritesService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpPut]
  [Authorize]

  public async Task<ActionResult<Account>> UpdateAccount(Account userData)
  {
    try
    {
      Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userEmail = user.Email;
      Account userUpdated = _accountService.Edit(userData, userEmail);
      return userUpdated;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("favorites")]
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
}
