public sealed record UserModel(
    string Id,
    string FullName,
    string Email,
    UserPreferencesModel Preferences
);