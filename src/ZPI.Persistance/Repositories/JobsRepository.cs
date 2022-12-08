using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Extensions;
using RestSharp;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Persistance.Entities;
using ZPI.Persistance.Mappings;
using ZPI.Persistance.ZPIDb;

namespace ZPI.Persistance.Repositories;

public class JobsRepository : IJobsRepository
{
    private const string apiUrl = "http://104.45.159.232:8000";
    private readonly ZPIDbContext context;
    private readonly IUsersRepository usersRepository;
    private readonly IUserPreferencesRepository userPreferencesRepository;
    private readonly IWalletRepository walletRepository;
    private readonly IUserAssetsRepository userAssetsRepository;
    private readonly IPersistanceMapper mapper;
    public JobsRepository(ZPIDbContext context, IPersistanceMapper mapper, IUsersRepository usersRepository, IUserAssetsRepository userAssetsRepository, IWalletRepository walletRepository, IUserPreferencesRepository userPreferencesRepository)
    {
        this.context = context;
        this.mapper = mapper;
        this.usersRepository = usersRepository;
        this.userAssetsRepository = userAssetsRepository;
        this.walletRepository = walletRepository;
        this.userPreferencesRepository = userPreferencesRepository;
    }

    public async Task SendRaportData()
    {
        var weekAgoDate = SystemClock.Instance.GetCurrentInstant().InUtc().Date.Minus(Period.FromDays(7));

        var users = await usersRepository.GetAll();
        var userPreferenecsWithWeeklyReports = await context.UserPreferences.Where(preference => preference.WeeklyReports).ToListAsync();

        var reqBody = new List<ReportWorkerCommand>() { };
        foreach (var userPreference in userPreferenecsWithWeeklyReports)
        {
            var walletValue = await walletRepository.GetAsync(new IWalletRepository.GetWallet(userPreference.UserId));
            var walletValueWeekAgo = (await walletRepository.SearchAsync(new IWalletRepository.GetWallets(weekAgoDate, weekAgoDate, userPreference.UserId))).FirstOrDefault();
            var userAssets = await userAssetsRepository.SearchAsync(new IUserAssetsRepository.GetUserAssets(userPreference.UserId));

            // nie wysylamy raportyu jak pajac nie ma assetow xd i elo
            var biggestUserAsset = userAssets.MaxBy(a => a.OriginValue);
            var email = users.First(u => u.UserId == userPreference.UserId).Email;

            if (biggestUserAsset is not null)
            {
                reqBody.Add(new(email, walletValue.total, walletValueWeekAgo?.Value, biggestUserAsset.Asset.FriendlyName, biggestUserAsset.UserCurrencyValue, userPreference.PreferenceCurrency));
            }
        }
        var json = JsonConvert.SerializeObject(reqBody);
        var client = new RestClient(apiUrl);
        var request = new RestRequest("api/report/");
        request.AddBody(json);
        var a = await client.PostAsync(request);
    }
}