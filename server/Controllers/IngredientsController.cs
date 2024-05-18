namespace allspice.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0Provider;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo.Id);
            return Ok(ingredient);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult<string>> TrashIngredient(int ingredientId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _ingredientsService.TrashIngredient(ingredientId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{ingredientId}")]
    public ActionResult<Ingredient> GetIngredientById(int ingredientId)
    {
        try
        {
            Ingredient ingredient = _ingredientsService.GetIngredientById(ingredientId);
            return Ok(ingredient);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}