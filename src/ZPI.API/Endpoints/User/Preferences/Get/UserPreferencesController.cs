using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Preferences.Get;

[ApiController]
[Route("api/users")]
public sealed class UserPreferencesController : UseCaseController<GetUserPreferencesUseCase, GetUserPreferencesPresenter>
{
    public UserPreferencesController(ILogger logger, GetUserPreferencesUseCase useCase, GetUserPreferencesPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("me/preferences", Name = nameof(GetUserPreferences))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(UserPreferencesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetUserPreferences()
    {
        await useCase.Execute(new GetUserPreferencesUseCase.Input("masno"), presenter);
        return await presenter.GetResultAsync();
    }
}
