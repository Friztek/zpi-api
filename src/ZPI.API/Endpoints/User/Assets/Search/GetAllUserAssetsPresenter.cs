using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Assets.Get;
public sealed class GetAllUserAssetsPresenter : ActionResultPresenterBase, GetAllUserAssetsUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public GetAllUserAssetsPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success(IEnumerable<UserAssetModel> userAssets) => SetResult(ActionResultFactory.Ok200(mapper.Map<IEnumerable<UserAssetDto>>(userAssets)));

    public void UnknownError(Exception exception) => SetException(exception);

    public void UserNotFound(UserNotFoundException exception) => SetResult(ActionResultFactory.NotFound404(exception.Message));
}