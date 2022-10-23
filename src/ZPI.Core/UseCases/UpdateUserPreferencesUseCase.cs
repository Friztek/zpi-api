using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class UpdateUserPreferencesUseCase : IUseCase<UpdateUserPreferencesUseCase.Input, UpdateUserPreferencesUseCase.IOutput>
{
    private readonly IUserPreferencesRepository repository;

    public UpdateUserPreferencesUseCase(IUserPreferencesRepository repository)
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
        UserPreferencesModel UpdateModel
    ) : IUserPreferencesRepository.UpdateUserPreferences(UserId, UpdateModel), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(UserPreferencesModel userPreferences);
        public void UserNotFound(UserNotFoundException exception);
        public void UnknownError(Exception exception);
    }
}
