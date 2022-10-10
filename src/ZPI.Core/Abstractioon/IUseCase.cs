namespace ZPI.Core.Abstraction;
public interface IUseCase
{
    Task Execute(object inputPort, object outputPort);
}

public interface IUseCase<in TInputPort, TOutputPort> : IUseCase
    where TInputPort : IInputPort
    where TOutputPort : IOutputPort
{
    Task Execute(TInputPort inputPort, TOutputPort outputPort);

    Task IUseCase.Execute(object inputPort, object outputPort) => this.Execute((TInputPort)inputPort, (TOutputPort)outputPort);
}
