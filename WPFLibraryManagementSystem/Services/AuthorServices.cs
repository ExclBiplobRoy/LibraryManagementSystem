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
    public class AuthorService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:7099/";

        public AuthorService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.ReadAuthors}";
                var response = await _client.GetAsync(requestRoute);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Author>>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching authors: {ex.Message}");
            }
        }

        public async Task CreateAuthorAsync(Author author)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.CreateAuthor}";
                var json = JsonConvert.SerializeObject(author);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the author: {ex.Message}");
            }
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.UpdateAuthor.Replace("{key}", author.AuthorID.ToString())}";
                string jsonAuthor = JsonConvert.SerializeObject(author);
                HttpContent content = new StringContent(jsonAuthor, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the author: {ex.Message}");
            }
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.DeleteAuthor.Replace("{key}", authorId.ToString())}?id={authorId}";
                HttpResponseMessage response = await _client.DeleteAsync(requestRoute);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the author: {ex.Message}");
            }
        }
    }
}