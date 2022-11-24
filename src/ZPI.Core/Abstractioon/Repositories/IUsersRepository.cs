using NodaTime;
using ZPI.Core.Domain;
using ZPI.Core.UseCases;

namespace ZPI.Core.Abstraction.Repositories;

public interface IUsersRepository
{

    public record PatchUser(string UserId, string? NewEmail, string? NewName);
    public Task<UserModel> UpdateEmail(string UserId, string Email);
    public Task<UserModel> UpdateFullName(string UserId, string Email);
    public Task<IEnumerable<UserModel>> GetAll();
}