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
    public class BookService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:7099/";

        public BookService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.ReadBooks}";
                var response = await _client.GetAsync(requestRoute);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Book>>(content);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching books: {ex.Message}");
            }
        }

        public async Task CreateBookAsync(Book book)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.CreateBook}";
                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the book: {ex.Message}");
            }
        }

        public async Task UpdateBookAsync(Book book)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.UpdateBook.Replace("{key}", book.BookID.ToString())}";
                string jsonBook = JsonConvert.SerializeObject(book);
                HttpContent content = new StringContent(jsonBook, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(requestRoute, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the book: {ex.Message}");
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            try
            {
                string requestRoute = $"{BaseUrl}{RouteConstants.BaseRoute}/{RouteConstants.DeleteBook.Replace("{key}", bookId.ToString())}?id={bookId}";
                HttpResponseMessage response = await _client.DeleteAsync(requestRoute);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the book: {ex.Message}");
            }
        }
    }
}
