using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class GetAllUserAssetsUseCase : IUseCase<GetAllUserAssetsUseCase.Input, GetAllUserAssetsUseCase.IOutput>
{
    private readonly IUserAssetsRepository repository;

    public GetAllUserAssetsUseCase(IUserAssetsRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var assets = await this.repository.SearchAsync(inputPort);
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
        string UserId
    ) : IUserAssetsRepository.GetUserAssets(UserId), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(IEnumerable<UserAssetModel> userAssets);
        public void UserNotFound(UserNotFoundException exception);
        public void UnknownError(Exception exception);
    }
}
