using ZPI.Core.Abstraction;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;

namespace ZPI.Core.UseCases;

public sealed class SendRaportsDataUseCase : IUseCase<SendRaportsDataUseCase.Input, SendRaportsDataUseCase.IOutput>
{
    private readonly IJobsRepository repository;

    public SendRaportsDataUseCase(IJobsRepository repository)
    {
        this.repository = repository;
    }

    public async Task Execute(Input inputPort, IOutput outputPort)
    {
        try
        {
            await this.repository.SendRaportData();
            outputPort.Success();
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
