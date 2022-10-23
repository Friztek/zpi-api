using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IUserPreferencesRepository :
    IGetRepository<IUserPreferencesRepository.GetUserPreferences, UserPreferencesModel>,
    IUpdateRepository<IUserPreferencesRepository.UpdateUserPreferences, UserPreferencesModel>
{
    public record GetUserPreferences(string UserId);
    public record UpdateUserPreferences(string UserId, UserPreferencesModel UpdateModel);
}