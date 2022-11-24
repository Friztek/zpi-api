using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using NodaTime.Extensions;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.UseCases;
using ZPI.Persistance.ZPIDb;

public class ProcAlertsWorker : BackgroundService
{
    private readonly IServiceProvider services;

    public ProcAlertsWorker(IServiceProvider services)
    {
        this.services = services;
    }

    private static int CalculateWaitTimeInMilliseconds()
    {
        var now = SystemClock.Instance.InUtc().GetCurrentLocalDateTime();
        var nextSaturday = now.Next(IsoDayOfWeek.Saturday).Date.AtMidnight();
        var diff = (nextSaturday - now).ToDuration();

        return (int)diff.TotalMilliseconds;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var waitTime = CalculateWaitTimeInMilliseconds();
            await Task.Delay(waitTime, stoppingToken);
            await DoBackupAsync();
        }
    }
    private async Task DoBackupAsync()
    {
        using var scope = services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ZPIDbContext>();
        var usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();

        var users = await usersRepository.GetAll();

    }
}