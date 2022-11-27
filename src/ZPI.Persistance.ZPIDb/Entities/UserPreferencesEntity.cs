namespace ZPI.Persistance.Entities;

public class UserPreferencesEntity
{
    public string UserId { get; set; }
    public string PreferenceCurrency {get; set; }
    public bool WeeklyReports {get; set; }
    public bool AlertsOnEmail {get; set; }
    public bool IsDefault {get; set; }
}