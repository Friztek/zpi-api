using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Users.Add;
public sealed class AddUserPresenter : ActionResultPresenterBase, AddUserUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public AddUserPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success(UserPreferencesModel model) => SetResult(ActionResultFactory.NoContent());

    public void UnknownError(Exception exception) => SetException(exception);
}