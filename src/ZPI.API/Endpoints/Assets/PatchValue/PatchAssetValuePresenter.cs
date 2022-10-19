using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Assets.PatchValue;
public sealed class PatchAssetValuePresenter : ActionResultPresenterBase, PatchAssetValueUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public PatchAssetValuePresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success(AssetValueModel model) => SetResult(ActionResultFactory.NoContent());

    public void UnknownError(Exception exception) => SetException(exception);
}