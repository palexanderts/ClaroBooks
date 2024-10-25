using Domain.Books;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace Data.Repositories
{
    public sealed class RepositoryBooks : IRepositoryBoocks
    {
        private readonly BoocksDbContext _boocksDbContext;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RepositoryBooks(
                BoocksDbContext boocksDbContext,
                HttpClient httpClient,
                IConfiguration configuration)
        {
            this._boocksDbContext = boocksDbContext;
            this._httpClient = httpClient;
            this._configuration = configuration;

            this._boocksDbContext.Database.EnsureCreated();
        }

        private bool ConnectToOutside => _configuration.GetValue<bool>("outside", false);

        public async Task AddAsync(Book book)
        {
            if (ConnectToOutside)
            {
                var httpResult = await _httpClient.PostAsJsonAsync<Book>(string.Empty, book);
                httpResult.EnsureSuccessStatusCode();
            }
            else
            {
                await _boocksDbContext.Books.AddAsync(book);
                await _boocksDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (ConnectToOutside)
            {
                var requestUri = $"Books/{id}";
                var httpResult = await _httpClient.DeleteAsync(requestUri);
                httpResult.EnsureSuccessStatusCode();
            }
            else
            {
                var book = await _boocksDbContext.Books.FindAsync(id);
                _boocksDbContext.Books.Remove(book);
                await _boocksDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            if (ConnectToOutside)
            {
                var httpResult = await _httpClient.GetFromJsonAsync<IEnumerable<Book>>(string.Empty);
                return httpResult;
            }
            else
            {
                return _boocksDbContext.Books;
            }
        }

        public async Task<Book> GetAsync(int id)
        {
            try
            {
                if (ConnectToOutside)
                {
                    var requestUri = $"Books/{id}";
                    var httpResult = await _httpClient.GetFromJsonAsync<Book>(requestUri);

                    return httpResult;
                }
                else
                {
                    return await _boocksDbContext.Books.FindAsync(id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task UpdateAsync(Book book)
        {
            if (ConnectToOutside)
            {
                var requestUri = $"Books/{book.Id}";
                var httpResult = await _httpClient.PutAsJsonAsync<Book>(requestUri, book);
                httpResult.EnsureSuccessStatusCode();
            }
            else
            {
                _boocksDbContext.Update(book);
                await _boocksDbContext.SaveChangesAsync();
            }
        }
    }
}
