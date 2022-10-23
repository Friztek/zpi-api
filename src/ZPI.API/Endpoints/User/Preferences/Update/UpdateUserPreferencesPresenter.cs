using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.API.Utils;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Preferences.Update;
public sealed class UpdateUserPreferencesPresenter : ActionResultPresenterBase, UpdateUserPreferencesUseCase.IOutput
{
    private readonly IAPIMapper mapper;
    public UpdateUserPreferencesPresenter(ILogger logger, IAPIMapper mapper) : base(logger)
    {
        this.mapper = mapper;
    }

    public void Success(UserPreferencesModel userPreferences) => SetResult(ActionResultFactory.Ok200(mapper.Map<UserPreferencesDto>(userPreferences)));

    public void UnknownError(Exception exception) => SetException(exception);

    public void UserNotFound(UserNotFoundException exception) => SetResult(ActionResultFactory.NotFound404(exception.Message));
}