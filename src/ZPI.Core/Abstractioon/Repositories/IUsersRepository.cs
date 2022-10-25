using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IUsersRepository :
    IGetRepository<string, UserModel>,
    IUpdateRepository<IUsersRepository.UpdateEmail, UserModel>,
    IUpdateRepository<IUsersRepository.UpdateName, UserModel>
{
    public record PatchUser(string UserId, string? NewEmail, string? NewName);
    public record UpdateEmail(string UserId, string Email);
    public record UpdateName(string UserId, string Name);
}