using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.API.Mappings;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Password.Reset;

[ApiController]
[Route("api/users")]
public sealed class UsersController : UseCaseController<ResetUserPasswordUseCase, ResetUserPasswordPresenter>
{
    private readonly IAPIMapper mapper;
    private readonly IUserInfoService service;
    public UsersController(ILogger logger, ResetUserPasswordUseCase useCase, ResetUserPasswordPresenter presenter, IAPIMapper mapper, IUserInfoService service)
        : base(logger, useCase, presenter)
    {
        this.mapper = mapper;
        this.service = service;
    }

    [HttpPost("me/password", Name = nameof(ResetUserPassword))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> ResetUserPassword()
    {
        var userEmail = service.GetCurrentUserEmail();
        await useCase.Execute(new ResetUserPasswordUseCase.Input(userEmail), presenter);
        return await presenter.GetResultAsync();
    }
}
