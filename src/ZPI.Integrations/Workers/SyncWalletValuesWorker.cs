using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using NodaTime.Extensions;
using ZPI.Core.UseCases;

public class WorkerService : BackgroundService
{
    private readonly PeriodicTimer timer = new(TimeSpan.FromSeconds(10));
    private readonly IServiceProvider services;

    public WorkerService(IServiceProvider services)
    {
        this.services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            await DoJobAsync();
        }
    }
    private async Task DoJobAsync()
    {
        using var scope = services.CreateScope();

        var useCase = scope.ServiceProvider.GetRequiredService<SyncWalletValuesUseCase>();

        await useCase.Execute(new SyncWalletValuesUseCase.Input(), new SyncWalletValuesPresenter());

    }
}