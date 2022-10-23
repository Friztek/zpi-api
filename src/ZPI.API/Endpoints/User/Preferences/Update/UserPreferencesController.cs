using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Preferences.Update;

[ApiController]
[Route("api/users")]
public sealed class UserPreferencesController : UseCaseController<UpdateUserPreferencesUseCase, UpdateUserPreferencesPresenter>
{
    private readonly IAPIMapper mapper;
    public UserPreferencesController(ILogger logger, UpdateUserPreferencesUseCase useCase, UpdateUserPreferencesPresenter presenter, IAPIMapper mapper)
        : base(logger, useCase, presenter)
    {
        this.mapper = mapper;
    }

    [HttpPut("me/preferences", Name = nameof(UpdateUserPreferences))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(UserPreferencesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> UpdateUserPreferences(UpdateUserPreferencesDto dto)
    {
        await useCase.Execute(new UpdateUserPreferencesUseCase.Input("masno", mapper.Map<UserPreferencesModel>(dto)), presenter);
        return await presenter.GetResultAsync();
    }
}
