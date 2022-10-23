using NodaTime;
using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class SearchAssetValuesUseCase : IUseCase<SearchAssetValuesUseCase.Input, SearchAssetValuesUseCase.IOutput>
{
    private readonly IAssetValuesRepository repository;

    public SearchAssetValuesUseCase(IAssetValuesRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var assetValues = await this.repository.SearchAsync(inputPort);
            outputPort.Success(assetValues);
        }
        catch (Exception e)
        {
            outputPort.UnknownError(e);
        }
    }

    public sealed record class Input(
        string AssetName,
        OffsetDate? From,
        OffsetDate? To
    ) : IAssetValuesRepository.SearchAssetValues(AssetName, From, To), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<AssetValueModel> assetValues);
        public void UnknownError(Exception exception);
    }
}
