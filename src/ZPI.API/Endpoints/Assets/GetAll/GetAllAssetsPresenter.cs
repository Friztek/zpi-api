using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Assets.GetAll;
public sealed class GetAllAssetsPresenter : ActionResultPresenterBase, GetAllAssetsUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public GetAllAssetsPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success(IEnumerable<AssetModel> assets) => SetResult(ActionResultFactory.Ok200(mapper.Map<IEnumerable<AssetDto>>(assets)));

    public void UnknownError(Exception exception) => SetException(exception);
}