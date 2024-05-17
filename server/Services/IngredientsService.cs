namespace allspice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repository;

    public IngredientsService(IngredientsRepository repository)
    {
        _repository = repository;
    }
}