using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Assets.Get;

[ApiController]
[Route("api/users")]
public sealed class UserAssetsController : UseCaseController<GetAllUserAssetsUseCase, GetAllUserAssetsPresenter>
{
    private readonly IUserInfoService _userInfoService;
    public UserAssetsController(ILogger logger, GetAllUserAssetsUseCase useCase, GetAllUserAssetsPresenter presenter, IUserInfoService userInfoService)
        : base(logger, useCase, presenter)
    {
        _userInfoService = userInfoService;
    }

    [HttpGet("me/assets", Name = nameof(GetAllUserAssets))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<UserAssetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllUserAssets()
    {
        var userId = _userInfoService.GetCurrentUserId();
        await useCase.Execute(new GetAllUserAssetsUseCase.Input(userId), presenter);
        return await presenter.GetResultAsync();
    }
}
