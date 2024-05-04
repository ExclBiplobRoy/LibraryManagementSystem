using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Newtonsoft.Json;
using Utilities;

namespace WPFLibraryManagementSystem.Services
{
    public class MemberService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:7099/";

        public MemberService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Member>> GetMembersAsync()
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.ReadMembers}";
                var response = await _client.GetAsync(requestRoute);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Member>>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching members: {ex.Message}");
            }
        }

        public async Task CreateMemberAsync(Member member)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.CreateMember}";
                var json = JsonConvert.SerializeObject(member);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the member: {ex.Message}");
            }
        }

        public async Task UpdateMemberAsync(Member member)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.UpdateMember.Replace("{key}", member.MemberID.ToString())}";
                string jsonMember = JsonConvert.SerializeObject(member);
                HttpContent content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the member: {ex.Message}");
            }
        }

        public async Task DeleteMemberAsync(int memberId)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.DeleteMember.Replace("{key}", memberId.ToString())}?id={memberId}";
                HttpResponseMessage response = await _client.DeleteAsync(requestRoute);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the member: {ex.Message}");
            }
        }
    }
}
