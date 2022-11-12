using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZPI.API.DTOs;
using ZPI.Core.Abstraction.Repositories;


namespace ZPI.API.Endpoints.Alerts.Get
{
    [ApiController]
    [Route("api/alerts")]
    [EnableCors("frontend_cors")]
    public class AlertController : ControllerBase
    {
        private static readonly HttpClient client = new();
        private const string apiUrl = "http://127.0.0.1:8000/api/alert/";
        private readonly IAssetValuesRepository repository;

        private readonly IUserInfoService service;

        public AlertController(IAssetValuesRepository repository, IUserInfoService service)
        {
            this.repository = repository;
            this.service = service;
        }

        [HttpGet]
        public async Task<Object> Get()
        {
            var email = service.GetCurrentUserEmail();
            var respone = await client.GetStringAsync(apiUrl + "?email=" + email);
            var model = JsonConvert.DeserializeObject<List<AlertWithActiveDto>>(respone);
            return model;
        }

        [HttpPost]
        public async Task<Object> Post(AlertDto alert)
        {
            var email = service.GetCurrentUserEmail();
            var asset_name = alert.OriginAssetName;
            var asset = await this.repository.GetAsync(new IAssetValuesRepository.GetAssetValue(asset_name));
            AlertWithEmailDto alertWithEmail = new AlertWithEmailDto
            {
                TargetValue = alert.Value,
                Email = email,
                OriginAssetName = alert.OriginAssetName,
                TargetCurrency = alert.Currency,
                CurrentValue = asset.Value
            };
            var json = JsonConvert.SerializeObject(alertWithEmail);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            int statusCode = (int)response.StatusCode;
            HttpContext.Response.StatusCode = statusCode;
            if (statusCode == 200)
                return true;
            return false;
        }

        [HttpDelete("{id}")]
        public async Task<Object> Delete(int id)
        {
            var email = service.GetCurrentUserEmail();
            var response = await client.DeleteAsync(apiUrl + "?id=" + id + "&email=" + email);
            int statusCode = (int)response.StatusCode;
            HttpContext.Response.StatusCode = statusCode;
            if (statusCode == 200)
                return true;
            return false;
        }
    }
}