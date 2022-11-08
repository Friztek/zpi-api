using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class PatchUserUseCase : IUseCase<PatchUserUseCase.Input, PatchUserUseCase.IOutput>
{
    private readonly IUsersRepository repository;

    public PatchUserUseCase(IUsersRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            // if (inputPort.NewEmail is string newEmail)
            // {
            //     await this.repository.UpdateEmail(inputPort.UserId, newEmail);
            // }

            if (inputPort.NewName is string newName)
            {
                await this.repository.UpdateFullName(inputPort.UserId, newName);
            }

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
        string? NewEmail,
        string? NewName
    ) : IUsersRepository.PatchUser(UserId, NewEmail, NewName), IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success();
        public void UserNotFound(UserNotFoundException exception);
        public void UnknownError(Exception exception);
    }
}
