using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class ResetUserPasswordUseCase : IUseCase<ResetUserPasswordUseCase.Input, ResetUserPasswordUseCase.IOutput>
{
    private readonly IUsersRepository repository;

    public ResetUserPasswordUseCase(IUsersRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            await this.repository.ResetPassword(inputPort.Email);
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
        string Email
    ) : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success();
        public void UserNotFound(UserNotFoundException exception);
        public void UnknownError(Exception exception);
    }
}
