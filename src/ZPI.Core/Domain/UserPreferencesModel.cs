namespace ZPI.Core.Domain;

public sealed record UserPreferencesModel(
    string PreferenceCurrency,
    bool WeeklyReports,
    bool AlertsOnEmail
);