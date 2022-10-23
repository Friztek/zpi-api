using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.AssetValues.Add;
public sealed class AddAssetValuePresenter : ActionResultPresenterBase, AddAssetValueUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public AddAssetValuePresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success(AssetValueModel model) => SetResult(ActionResultFactory.NoContent());

    public void UnknownError(Exception exception) => SetException(exception);
}