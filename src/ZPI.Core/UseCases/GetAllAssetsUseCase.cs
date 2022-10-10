using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;

namespace ZPI.Core.UseCases;

public sealed class GetAllAssetsUseCase : IUseCase<GetAllAssetsUseCase.Input, GetAllAssetsUseCase.IOutput>
{
    private readonly IAssetsRepository repository;

    public GetAllAssetsUseCase(IAssetsRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var assets = await this.repository.SearchAsync(new IAssetsRepository.GetAllAssets());
            outputPort.Success(assets);
        }
        catch (Exception e)
        {
            outputPort.UnknownError(e);
        }
    }

    public sealed record class Input() : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<AssetModel> assets);
        public void UnknownError(Exception exception);
    }
}
