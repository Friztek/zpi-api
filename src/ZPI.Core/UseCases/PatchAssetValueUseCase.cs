using NodaTime;
using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;

namespace ZPI.Core.UseCases;

public sealed class PatchAssetValueUseCase : IUseCase<PatchAssetValueUseCase.Input, PatchAssetValueUseCase.IOutput>
{
    private readonly IAssetsRepository repository;

    public PatchAssetValueUseCase(IAssetsRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var asset = await this.repository.UpdateAsync(inputPort);
            outputPort.Success(asset);
        }
        catch (Exception e)
        {
            outputPort.UnknownError(e);
        }
    }

    public sealed record class Input(
        string AssetName,
        double Value,
        OffsetDateTime TimeStamp
    ) : IAssetsRepository.PatchAssetValue(
        AssetName,
        Value,
        TimeStamp
    ), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(AssetValueModel asset);
        public void UnknownError(Exception exception);
    }
}
