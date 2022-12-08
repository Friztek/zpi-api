using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Assets.GetAll;
public sealed class SendRaportPresenter : ActionResultPresenterBase, SendRaportsDataUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public SendRaportPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success() => SetResult(ActionResultFactory.NoContent());

    public void UnknownError(Exception exception) => SetException(exception);
}