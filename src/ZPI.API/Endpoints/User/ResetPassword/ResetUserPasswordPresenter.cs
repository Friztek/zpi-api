using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Password.Reset;
public sealed class ResetUserPasswordPresenter : ActionResultPresenterBase, ResetUserPasswordUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public ResetUserPasswordPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success() => SetResult(ActionResultFactory.Ok200());

    public void UnknownError(Exception exception) => SetException(exception);

    public void UserNotFound(UserNotFoundException exception) => SetResult(ActionResultFactory.NotFound404(exception.Message));
}