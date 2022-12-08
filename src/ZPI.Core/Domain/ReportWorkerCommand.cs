namespace ZPI.Core.Domain;

public sealed record ReportWorkerCommand(
    string email,
    double currenctWalletValue,
    double? walletValueWeekAgo,
    string biggestAssetName,
    double biggestAssetValue,
    string currencyPreference
);