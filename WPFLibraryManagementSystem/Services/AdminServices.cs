using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Utilities;

namespace WPFLibraryManagementSystem.Services
{
    public class AdminService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:7099/";

        public AdminService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Admin>> GetAdminsAsync()
        {
            string RequestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.RaedAdmins}";
            var response = await _client.GetAsync(RequestRoute);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Admin>>(content);
        }
    }
}