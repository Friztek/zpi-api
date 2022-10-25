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
    public UserAssetsController(ILogger logger, GetAllUserAssetsUseCase useCase, GetAllUserAssetsPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("me/assets", Name = nameof(GetAllUserAssets))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<UserAssetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> GetAllUserAssets()
    {
        await useCase.Execute(new GetAllUserAssetsUseCase.Input("masno"), presenter);
        return await presenter.GetResultAsync();
    }
}
