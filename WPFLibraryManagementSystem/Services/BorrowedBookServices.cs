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
    public class BorrowedBookService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:7099/";

        public BorrowedBookService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<BorrowedBook>> GetBorrowedBooksAsync()
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.ReadBorrowedBooks}";
                var response = await _client.GetAsync(requestRoute);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BorrowedBook>>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching borrowed books: {ex.Message}");
            }
        }

        public async Task CreateBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.CreateBorrowedBook}";
                var json = JsonConvert.SerializeObject(borrowedBook);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the borrowed book: {ex.Message}");
            }
        }

        public async Task UpdateBorrowedBookAsync(BorrowedBook borrowedBook)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.UpdateBorrowedBook.Replace("{key}", borrowedBook.BorrowID.ToString())}";
                string jsonBorrowedBook = JsonConvert.SerializeObject(borrowedBook);
                HttpContent content = new StringContent(jsonBorrowedBook, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the borrowed book: {ex.Message}");
            }
        }

        public async Task DeleteBorrowedBookAsync(int borrowedBookId)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.DeleteBorrowedBook.Replace("{key}", borrowedBookId.ToString())}?id={borrowedBookId}";
                HttpResponseMessage response = await _client.DeleteAsync(requestRoute);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the borrowed book: {ex.Message}");
            }
        }
    }
}
