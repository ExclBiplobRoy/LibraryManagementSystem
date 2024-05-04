using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<Domain.Entities.Admin> AuthenticateAsync(string email, string password)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.AdminLogin.Replace("{key}", email + "," + password)}";
                var response = await _client.GetAsync(requestRoute);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Domain.Entities.Admin>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while authenticating: {ex.Message}");
            }
        }

        public async Task<List<Domain.Entities.Admin>> GetAdminsAsync()
        {
            string RequestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.RaedAdmins}";
            var response = await _client.GetAsync(RequestRoute);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Domain.Entities.Admin>>(content);
        }

        public async Task CreateAdminAsync(Domain.Entities.Admin admin)
        {
            try
            {
                string RequestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.CreateAdmin}";

                var json = JsonConvert.SerializeObject(admin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(RequestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the admin: {ex.Message}");
            }
        }

        public async Task UpdateAdminAsync(Domain.Entities.Admin admin)
        {
            try
            {
                string RequestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.UpdateAdmin.Replace("{key}", admin.AdminID.ToString())}";
                string jsonAdmin = JsonConvert.SerializeObject(admin);
                HttpContent content = new StringContent(jsonAdmin, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(RequestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the admin: {ex.Message}");
            }
        }

        public async Task DeleteAdminAsync(int adminId)
        {
            try
            {
                string RequestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.DeleteAdmin.Replace("{key}", adminId.ToString())}?id={adminId}";

                HttpResponseMessage response = await _client.DeleteAsync(RequestRoute);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}