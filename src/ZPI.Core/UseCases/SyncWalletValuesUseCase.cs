using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class SyncWalletValuesUseCase : IUseCase<SyncWalletValuesUseCase.Input, SyncWalletValuesUseCase.IOutput>
{
    private readonly IWalletRepository repository;

    public SyncWalletValuesUseCase(IWalletRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            await repository.SyncUserWallets();
        }
        catch (Exception e)
        {
            outputPort.UnknownError(e);
        }
    }

    public sealed record class Input() : IInputPort;

    public interface IOutput : IOutputPort
    {
        public void Success();
        public void UnknownError(Exception exception);
    }
}
