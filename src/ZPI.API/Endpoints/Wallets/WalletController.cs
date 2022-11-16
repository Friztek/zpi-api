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
using ZPI.API.DTOs;

namespace ZPI.API.Endpoints.AssetValues.Get
{
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IWalletRepository repository;
    private readonly IUserInfoService service;
    private readonly IAPIMapper mapper;
    public WalletController(IWalletRepository repository, IUserInfoService service, IAPIMapper mapper) { 
        this.repository = repository;
        this.service = service;
        this.mapper = mapper;
    }

    [Route("api/wallet")]
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<WalletDto>), StatusCodes.Status200OK)]
    public async Task<Object> SearchTransactions(LocalDate? from, LocalDate? to)
    {
        var id = service.GetCurrentUserId();
        var transactions = await this.repository.SearchAsync(new IWalletRepository.GetWallets(from, to, id));
        var model = this.mapper.Map<List<WalletDto>>(transactions);
        return model;
    }

    [Route("api/wallet/total")]
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(FullWalletDto), StatusCodes.Status200OK)]
    public async Task<Object> GetTotalWallet()
    {
        var id = service.GetCurrentUserId();
        (double all_assets, double currency_assets, double crypto_assets, double metal_assets) = await this.repository.GetAsync(new IWalletRepository.GetWallet(id));
        FullWalletDto walletValue = new FullWalletDto
            {
                TotalValue = all_assets,
                CryptoTotalValue = crypto_assets,
                CurrencyTotalValue = currency_assets,
                MetalTotalValue = metal_assets
            };
        return walletValue;
    }
}
}