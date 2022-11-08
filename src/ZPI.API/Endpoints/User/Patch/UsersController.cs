using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Users.Patch;

[ApiController]
[Route("api/users")]
public sealed class UsersController : UseCaseController<PatchUserUseCase, PatchUserPresenter>
{
    private IUserInfoService userInfoService;
    public UsersController(ILogger logger, PatchUserUseCase useCase, PatchUserPresenter presenter, IUserInfoService userInfoService)
        : base(logger, useCase, presenter)
    {
        this.userInfoService = userInfoService;
    }

    [HttpPatch("me", Name = nameof(PatchUserInfo))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(UserPreferencesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> PatchUserInfo(PatchUser dto)
    {
        var userId = userInfoService.GetCurrentUserId();
        await useCase.Execute(new PatchUserUseCase.Input(userId, dto.Email, dto.FullName), presenter);
        return await presenter.GetResultAsync();
    }
}
