using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Assets.Patch;
[ApiController]
[Route("api/users")]
public sealed class UserAssetsController : UseCaseController<PatchUserAssetsUseCase, PatchUserAssetsPresenter>
{
    private readonly IAPIMapper mapper;
    private readonly IUserInfoService service;
    public UserAssetsController(ILogger logger, PatchUserAssetsUseCase useCase, PatchUserAssetsPresenter presenter, IAPIMapper mapper, IUserInfoService service)
        : base(logger, useCase, presenter)
    {
        this.mapper = mapper;
        this.service = service;
    }

    [HttpPatch("me/assets", Name = nameof(PatchUserAssets))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<UserAssetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> PatchUserAssets(IEnumerable<PatchUserAssetsDto> dto)
    {
        var userId = service.GetCurrentUserId();
        await useCase.Execute(new PatchUserAssetsUseCase.Input(userId, mapper.Map<IEnumerable<IUserAssetsRepository.UserAssetTransaction>>(dto)), presenter);
        return await presenter.GetResultAsync();
    }
}
