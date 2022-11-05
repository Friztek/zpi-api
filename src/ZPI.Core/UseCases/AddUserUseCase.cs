using NodaTime;
using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;

namespace ZPI.Core.UseCases;

public sealed class AddUserUseCase : IUseCase<AddUserUseCase.Input, AddUserUseCase.IOutput>
{
    private readonly IUserPreferencesRepository repository;

    public AddUserUseCase(IUserPreferencesRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            var user = await this.repository.CreateAsync(inputPort);
            outputPort.Success(user);
        }
        catch (Exception e)
        {
            outputPort.UnknownError(e);
        }
    }

    public sealed record class Input(
        string UserId
    ) : IUserPreferencesRepository.AddUserFromAuth0(
        UserId
    ), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success(UserPreferencesModel user);
        public void UnknownError(Exception exception);
    }
}
