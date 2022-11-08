using NodaTime;
using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class GetAssetValuesUseCase : IUseCase<GetAssetValuesUseCase.Input, GetAssetValuesUseCase.IOutput>
{
    private readonly IAssetValuesRepository repository;

    public GetAssetValuesUseCase(IAssetValuesRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var assets = await this.repository.SearchAsync(new IAssetValuesRepository.GetAssetValues());
            outputPort.Success(assets);
        }
        catch (AssetNotFoundException e)
        {

        }
        catch (Exception e)
        {
            outputPort.UnknownError(e);
        }
    }


    public sealed record class Input() : IInputPort;
    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<AssetValueModel> assetValues);
        public void UnknownError(Exception exception);
    }
}