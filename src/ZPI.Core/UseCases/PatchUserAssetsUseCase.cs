using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class PatchUserAssetsUseCase : IUseCase<PatchUserAssetsUseCase.Input, PatchUserAssetsUseCase.IOutput>
{
    private readonly IUserAssetsRepository repository;

    public PatchUserAssetsUseCase(IUserAssetsRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var assets = await this.repository.UpdateAsync(inputPort);
            outputPort.Success(assets);
        }
        catch (UserNotFoundException e)
        {
            outputPort.UserNotFound(e);
        }
        catch (Exception e)
        {
            outputPort.UnknownError(e);
        }
    }

    public sealed record class Input(
        string UserId,
        IEnumerable<IUserAssetsRepository.UserAssetTransaction> Assets
    ) : IUserAssetsRepository.PatchUserAssets(UserId, Assets), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<UserAssetModel> userAssets);
        public void UserNotFound(UserNotFoundException exception);
        public void UnknownError(Exception exception);
    }
}
