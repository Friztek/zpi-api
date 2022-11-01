using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.AssetValues.Get
{
public sealed class GetAssetValuesPresenter : ActionResultPresenterBase, GetAssetValuesUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public GetAssetValuesPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success(IEnumerable<AssetValueModel> assetValues) => SetResult(ActionResultFactory.Ok200(mapper.Map<IEnumerable<AssetValueDto>>(assetValues)));

    public void UnknownError(Exception exception) => SetException(exception);
}
}