using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZPI.API.DTOs;
using ZPI.Core.Abstraction.Repositories;


namespace ZPI.API.Endpoints.Alerts.Get
{
    [ApiController]
    [Route("api/alerts")]
    public class AlertController : ControllerBase
    {
        private static readonly HttpClient client = new();
        private const string apiUrl = "http://127.0.0.1:8000/api/alert/";
        private readonly IAssetValuesRepository repository;

        private readonly IUserInfoService service;

        private readonly IUserPreferencesRepository userRepository;

        public AlertController(IAssetValuesRepository repository, IUserInfoService service, IUserPreferencesRepository userRepository)
        {
            this.repository = repository;
            this.service = service;
            this.userRepository = userRepository;
        }


        [HttpGet(Name = nameof(GetAllAllerts))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(IEnumerable<AlertDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAllerts()
        {
            var email = service.GetCurrentUserEmail();
            var respone = await client.GetStringAsync(apiUrl + "?email=" + email);
            return Ok(JsonConvert.DeserializeObject<List<AlertDto>>(respone));
        }

        [HttpPost(Name = nameof(AddNewAllert))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(AlertDto), StatusCodes.Status200OK)]
        public async Task<Object> AddNewAllert(AddAlertDto alert)
        {
            if (alert.Currency == alert.OriginAssetName)
            {
                HttpContext.Response.StatusCode = 400;
                return "Same target currency and origin asset name";
            }
            var email = service.GetCurrentUserEmail();
            var userId = service.GetCurrentUserId();
            var userPreference = await this.userRepository.GetAsync(new IUserPreferencesRepository.GetUserPreferences(userId));
            var asset_name = alert.OriginAssetName;
            var asset = await this.repository.GetAsync(new IAssetValuesRepository.GetAssetValue(asset_name));
            AlertWithEmailDto alertWithEmail = new AlertWithEmailDto
            {
                TargetValue = alert.Value,
                Email = email,
                OriginAssetName = alert.OriginAssetName,
                TargetCurrency = alert.Currency,
                CurrentValue = asset.Value,
                OnEmail = userPreference.AlertsOnEmail
            };
            var json = JsonConvert.SerializeObject(alertWithEmail);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            var contents = await response.Content.ReadAsStringAsync();
            int statusCode = (int)response.StatusCode;
            var model = JsonConvert.DeserializeObject<AlertDto>(contents);
            HttpContext.Response.StatusCode = statusCode;
            if (statusCode == 200)
                return model;
            return false;
        }

        [HttpDelete("{id}", Name = nameof(DeleteAlert))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<Object> DeleteAlert(int id)
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