using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Assets.Delete;
public sealed class DeleteUserAssetsPresenter : ActionResultPresenterBase, DeleteUserAssetUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public DeleteUserAssetsPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success() => SetResult(ActionResultFactory.NoContent());

    public void UnknownError(Exception exception) => SetException(exception);

    public void UserNotFound(UserNotFoundException exception) => SetResult(ActionResultFactory.NotFound404(exception.Message));
}