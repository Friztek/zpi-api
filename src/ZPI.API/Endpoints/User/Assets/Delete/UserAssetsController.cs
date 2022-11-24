using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Assets.Delete;

[ApiController]
[Route("api/users")]
public sealed class UserAssetsController : UseCaseController<DeleteUserAssetUseCase, DeleteUserAssetsPresenter>
{
    private readonly IUserInfoService service;
    public UserAssetsController(ILogger logger, DeleteUserAssetUseCase useCase, DeleteUserAssetsPresenter presenter, IUserInfoService service)
        : base(logger, useCase, presenter)
    { 
        this.service = service;
    }

    [HttpDelete("me/assets/{assetName}", Name = nameof(DeleteUserAsset))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<UserAssetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeleteUserAsset(string assetName, string? description)
    {
        var userId = service.GetCurrentUserId();
        await useCase.Execute(new DeleteUserAssetUseCase.Input(userId, assetName, description), presenter);
        return await presenter.GetResultAsync();
    }
}
