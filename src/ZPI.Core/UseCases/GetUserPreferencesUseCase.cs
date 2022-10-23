using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class GetUserPreferencesUseCase : IUseCase<GetUserPreferencesUseCase.Input, GetUserPreferencesUseCase.IOutput>
{
    private readonly IUserPreferencesRepository repository;

    public GetUserPreferencesUseCase(IUserPreferencesRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var assets = await this.repository.GetAsync(inputPort);
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
    ) : IUserPreferencesRepository.GetUserPreferences(UserId), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(UserPreferencesModel userPreferences);
        public void UserNotFound(UserNotFoundException exception);
        public void UnknownError(Exception exception);
    }
}
