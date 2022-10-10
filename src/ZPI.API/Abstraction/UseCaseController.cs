using ZPI.Core.Abstraction;

namespace ZPI.API.Abstraction;
public abstract class UseCaseController<TUseCase, TPresenter> : Microsoft.AspNetCore.Mvc.ControllerBase
    where TUseCase : IUseCase
{
    protected readonly TUseCase useCase;
    protected readonly TPresenter presenter;
    protected readonly ILogger logger;

    public UseCaseController(ILogger logger, TUseCase useCase, TPresenter presenter)
    {
        this.useCase = useCase;
        this.presenter = presenter;
        this.logger = logger;
    }
}