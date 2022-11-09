using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NodaTime;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.UseCases;
using ZPI.API.Mappings;

namespace ZPI.API.Endpoints.AssetValues.Get
{
[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionepository repository;
    private readonly IUserInfoService service;
    private readonly IAPIMapper mapper;
    public TransactionController(ITransactionepository repository, IUserInfoService service, IAPIMapper mapper) { 
        this.repository = repository;
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
    public async Task<Object> SearchTransactions(OffsetDate? from, OffsetDate? to)
    {
        var id = service.GetCurrentUserId();
        var transactions = await this.repository.SearchAsync(new ITransactionepository.GetTransactions(from, to, id));
        var model = this.mapper.Map<List<TransactionDto>>(transactions);
        return model;
    }
}
}