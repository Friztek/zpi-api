using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class DeleteUserAssetUseCase : IUseCase<DeleteUserAssetUseCase.Input, DeleteUserAssetUseCase.IOutput>
{
    private readonly IUserAssetsRepository repository;

    public DeleteUserAssetUseCase(IUserAssetsRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            await this.repository.DeleteAsync(inputPort);
            outputPort.Success();
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
        string AssetName
    ) : IUserAssetsRepository.DeleteUserAsset(UserId, AssetName), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success();
        public void UserNotFound(UserNotFoundException exception);
        public void UnknownError(Exception exception);
    }
}
