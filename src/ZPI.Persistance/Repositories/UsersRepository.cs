using Auth0.ManagementApi;
using Microsoft.EntityFrameworkCore;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Core.Exceptions;
using ZPI.Persistance.Entities;
using ZPI.Persistance.Mappings;
using ZPI.Persistance.ZPIDb;

namespace ZPI.Persistance.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly ZPIDbContext context;
    private readonly IPersistanceMapper mapper;
    private readonly ManagementApiClient managementApiClient;

    public UsersRepository(ZPIDbContext context, IPersistanceMapper mapper, IManagementConnection managementConnection)
    {
        this.context = context;
        this.mapper = mapper;
        this.managementApiClient = new ManagementApiClient("token", new Uri("http://base-path"), managementConnection);
    }

    public Task<UserModel> GetAsync(string getModel)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> UpdateAsync(IUsersRepository.UpdateEmail updateModel)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> UpdateAsync(IUsersRepository.UpdateName updateModel)
    {
        throw new NotImplementedException();
    }
}