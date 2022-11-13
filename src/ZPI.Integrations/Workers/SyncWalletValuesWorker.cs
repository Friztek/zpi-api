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
    private readonly IServiceProvider services;

    public WorkerService(IServiceProvider services)
    {
        this.services = services;
    }

    private static int CalculateWaitTimeInMilliseconds()
    {
        var now = SystemClock.Instance.InUtc().GetCurrentOffsetDateTime();
        var nextDay = new OffsetDate(now.Date.PlusDays(1), now.Offset).At(LocalTime.Midnight);

        return (int)(nextDay - now).TotalMilliseconds;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var waitTime = CalculateWaitTimeInMilliseconds();
            await Task.Delay(waitTime, stoppingToken);
            await DoBackupAsync();
            await Task.Delay(23132323, stoppingToken);
        }
    }
    private async Task DoBackupAsync()
    {
        using var scope = services.CreateScope();

        var useCase = scope.ServiceProvider.GetRequiredService<SyncWalletValuesUseCase>();

        await useCase.Execute(new SyncWalletValuesUseCase.Input(), new SyncWalletValuesPresenter());

    }
}