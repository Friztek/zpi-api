using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ZPI.Core.Abstraction.Integrations;
using ZPI.Core.Abstraction.Repositories;
using ZPI.Core.Domain;
using ZPI.Integrations.Configuration;

namespace ZPI.Integrations.Services;

public class UsersRepository : IUsersRepository
{
    private readonly ManagementApiClient managementApiClient;
    public UsersRepository(
        IManagementConnection managementConnection,
        IOptions<ManagementApiOptions> managementApiOptions,
        IAuth0ManagementTokenProvider tokenProvider
        )
    {
        var config = managementApiOptions.Value;
        var token = tokenProvider.GetTokenAsync().Result;
        this.managementApiClient = new ManagementApiClient(token, new Uri(config.BasePath), managementConnection);
    }

    public async Task<UserModel> UpdateEmail(string UserId, string Email)
    {
        UserUpdateRequest updateRequest = new()
        {
            Email = Email
        };

        var res = await this.managementApiClient.Users.UpdateAsync(UserId, updateRequest);
        return new UserModel(res.FullName, res.Email);
    }

    public async Task<UserModel> UpdateFullName(string UserId, string FullName)
    {
        UserUpdateRequest updateRequest = new()
        {
            FullName = FullName
        };

        var res = await this.managementApiClient.Users.UpdateAsync(UserId, updateRequest);

        return new UserModel(res.FullName, res.Email);
    }
}